using AutoMapper;
using Invoice.Contracts.Logger;
using Invoice.Contracts.Repositories;
using Invoice.Entities.ConfigurationModels;
using Invoice.Entities.Exceptions;
using Invoice.Entities.Models;
using Invoice.Service.Contracts.BusinessServices;
using Invoice.Service.Contracts.HelperServices;
using Invoice.Shared.Request;
using Invoice.Shared.Response;
using Microsoft.Extensions.Options;
using UBLSunatPE;

namespace Invoice.Service.BusinessServices;

public class VoidedDocumentsService : IVoidedDocumentsService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IDocumentGeneratorService _documentGeneratorService;
    private readonly ISunatService _sunatService;
    private readonly IOptions<SunatConfiguration> _configuration;

    public VoidedDocumentsService(IRepositoryManager repository, 
        ILoggerManager logger, 
        IMapper mapper, 
        IDocumentGeneratorService documentGeneratorService, 
        ISunatService sunatService,
        IOptions<SunatConfiguration> configuration)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
        _documentGeneratorService = documentGeneratorService;
        _sunatService = sunatService;
        _configuration = configuration;
    }

    public async Task<DocumentsResponse> CreateVoidedDocumentsAsync(Guid id, VoidedDocumentsRequest request, bool trackChanges)
    {
        var issuer = await GetIssuerAndCheckIfItExists(id, trackChanges);

        var invoices = await GetInvoicesBySerieAndCheckIfItExists(request.DocumentsToVoid, trackChanges);

        //Generate xml
        var voidedDocuments = _documentGeneratorService.GenerateVoidedDocumentsType(request, issuer);

        //Serialize to xml
        var xmlString = _sunatService.SerializeXmlDocument(typeof(VoidedDocumentsType), voidedDocuments);

        //Sign xml
        var xmlDoc = _sunatService.SignXml(xmlString, issuer, "RA");

        //Zip xml
        var xmlFile = $"{issuer.IssuerId}-{voidedDocuments.ID.Value}.xml";
        var byteZippedXml = _sunatService.ZipXml(xmlDoc, Path.GetFileName(xmlFile));

        //Send bill
        var zippedFile = xmlFile.Replace(".xml", ".zip");

        var ticketNumber = await _sunatService.SendSummary(_configuration.Value.UrlInvoice,
                _configuration.Value.Username,
                _configuration.Value.Password,
                zippedFile,
                byteZippedXml);

        if (ticketNumber != null)
        {
            foreach (var invoice in invoices)
            {
                invoice.Canceled = true;
                invoice.Ticket = ticketNumber;
                invoice.CanceledReason = "Voided document";
            }

            _repository.Ticket.CreateTicket(new Ticket
            {
                IssueDate = request.IssueDate,
                TicketType = "Voided",
                TicketNumber = ticketNumber,
                DocumentsXml = xmlDoc.OuterXml
            });

            await _repository.SaveAsync();

            var voidedDocumentsResponse = new DocumentsResponse
            {
                TicketType = "Voided",
                SummarySended = true,
                Ticket = ticketNumber
            };

            return voidedDocumentsResponse;
        }
        return null;
    }

    private async Task<Issuer> GetIssuerAndCheckIfItExists(Guid id, bool trackChanges)
    {
        var issuer = await _repository.Issuer.GetIssuerAsync(id, trackChanges);

        if (issuer is null)
            throw new IssuerNotFoundException(id);

        return issuer;
    }

    private async Task<IEnumerable<Entities.Models.Invoice>> GetInvoicesBySerieAndCheckIfItExists(IEnumerable<DocumentToVoidRequest> documentsToVoid, bool trackChanges)
    {
        var documentsToVoidRequest = new List<string>();
        var invoices = new List<Entities.Models.Invoice>();


        foreach (var documentToVoid in documentsToVoid)
        {
            var invoice = await _repository.Invoice.GetInvoiceBySerieAsync(documentToVoid.Serie, documentToVoid.SerialNumber, documentToVoid.CorrelativeNumber, trackChanges);

            if (invoice is null)
                documentsToVoidRequest.Add($"{documentToVoid.Serie}{documentToVoid.SerialNumber.ToString("00")}-{documentToVoid.CorrelativeNumber.ToString("00000000")}");

            invoices.Add(invoice);            
        }

        if (documentsToVoidRequest.Any())
            throw new InvoiceNotFoundException(documentsToVoidRequest);

        return invoices;
    }
}