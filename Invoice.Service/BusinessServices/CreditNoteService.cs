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

public class CreditNoteService : ICreditNoteService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IDocumentGeneratorService _documentGeneratorService;
    private readonly ISunatService _sunatService;
    private readonly IOptions<SunatConfiguration> _configuration;

    public CreditNoteService(IRepositoryManager repository, 
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

    public async Task<InvoiceResponse> CreateCreditNoteAsync(Guid id, NoteRequest request, bool trackChanges)
    {
        var issuer = await GetIssuerAndCheckIfItExists(id, trackChanges);

        var invoice = await GetInvoiceBySerieAndCheckIfItExists(request.NoteDetail.InvoiceSerie, request.NoteDetail.InvoiceSerialNumber, request.NoteDetail.InvoiceCorrelativeNumber, true);

        if (invoice.Canceled)
            throw new InvoiceCanceledException(request.NoteDetail.InvoiceSerie, request.NoteDetail.InvoiceSerialNumber, request.NoteDetail.InvoiceCorrelativeNumber);

        var creditNote = _documentGeneratorService.GenerateCreditNoteType(request, issuer);

        //Serialize to xml
        var xmlString = _sunatService.SerializeXmlDocument(typeof(CreditNoteType), creditNote);

        //Sign xml
        var xmlDoc = _sunatService.SignXml(xmlString, issuer, request.NoteDetail.DocumentType);

        //Zip xml
        var xmlFile = $"{issuer.IssuerId}-{request.NoteDetail.DocumentType}-{request.NoteDetail.Serie}{request.NoteDetail.SerialNumber.ToString("00")}-{request.NoteDetail.CorrelativeNumber.ToString("00000000")}.xml";
        var byteZippedXml = _sunatService.ZipXml(xmlDoc, Path.GetFileName(xmlFile));

        //Send bill
        var zippedFile = xmlFile.Replace(".xml", ".zip");
        var cdrByte = await _sunatService.SendBill(_configuration.Value.UrlInvoice,
                _configuration.Value.Username,
                _configuration.Value.Password,
                zippedFile,
                byteZippedXml);

        //Read response
        var responses = _sunatService.ReadResponse(cdrByte);

        //Save debit note
        if (responses.Any(x => x.Contains("aceptada")))
        {
            var invoiceDb = _mapper.Map<NoteRequest, Invoice.Entities.Models.Invoice>(request);
            invoiceDb.IssuerId = issuer.Id;
            invoiceDb.InvoiceXml = xmlDoc.OuterXml;
            invoiceDb.Accepted = true;
            invoiceDb.SunatResponse = cdrByte;
            invoiceDb.Observations = string.Join("|", responses);
            _repository.Invoice.CreateInvoice(invoiceDb);
            invoice.Canceled = true;
            invoice.CanceledReason = responses.FirstOrDefault(x => x.Contains("aceptada"));
            await _repository.SaveAsync();

            var invoiceResponse = _mapper.Map<Entities.Models.Invoice, InvoiceResponse>(invoiceDb);
            return invoiceResponse;
        }

        return null;
    }

    public async Task<InvoiceResponse> GetCreditNoteAsync(Guid id, bool trackChanges)
    {
        var creditNote = await GetInvoiceAndCheckIfItExists(id, trackChanges);

        var debitNoteResponse = _mapper.Map<Entities.Models.Invoice, InvoiceResponse>(creditNote);
        return debitNoteResponse;
    }

    private async Task<Entities.Models.Invoice> GetInvoiceAndCheckIfItExists(Guid id, bool trackChanges)
    {
        var invoice = await _repository.Invoice.GetInvoiceAsync(id, trackChanges);

        if (invoice is null)
            throw new InvoiceNotFoundException(id);

        return invoice;
    }

    private async Task<Entities.Models.Invoice> GetInvoiceBySerieAndCheckIfItExists(string serie, uint serialNumber, uint correlativeNumber, bool trackChanges)
    {
        var invoice = await _repository.Invoice.GetInvoiceBySerieAsync(serie, serialNumber, correlativeNumber, trackChanges);

        if (invoice is null)
            throw new InvoiceNotFoundException(serie, serialNumber, correlativeNumber);

        return invoice;
    }

    private async Task<Issuer> GetIssuerAndCheckIfItExists(Guid id, bool trackChanges)
    {
        var issuer = await _repository.Issuer.GetIssuerAsync(id, trackChanges);

        if (issuer is null)
            throw new IssuerNotFoundException(id);

        return issuer;
    }
}