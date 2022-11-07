using AutoMapper;
using Invoice.Contracts.Logger;
using Invoice.Contracts.Repositories;
using Invoice.Entities.Exceptions;
using Invoice.Entities.Models;
using Invoice.Service.Contracts.BusinessServices;
using Invoice.Service.Contracts.HelperServices;
using Invoice.Shared.Request;
using Invoice.Shared.Response;
using UBLSunatPE;

namespace Invoice.Service.BusinessServices;

public class DebitNoteService : IDebitNoteService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IDocumentGeneratorService _documentGeneratorService;
    private readonly ISunatService _sunatService;

    public DebitNoteService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IDocumentGeneratorService documentGeneratorService, ISunatService sunatService)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
        _documentGeneratorService = documentGeneratorService;
        _sunatService = sunatService;
    }

    public async Task<InvoiceResponse> CreateDebitNoteAsync(Guid id, NoteRequest request, bool trackChanges)
    {
        var issuer = await GetIssuerAndCheckIfItExists(id, trackChanges);

        var invoice = await GetInvoiceBySerieAndCheckIfItExists(request.NoteDetail.InvoiceSerie, request.NoteDetail.InvoiceSerialNumber, request.NoteDetail.InvoiceCorrelativeNumber, true);

        if (invoice.Canceled)
            throw new InvoiceCanceledException(request.NoteDetail.InvoiceSerie, request.NoteDetail.InvoiceSerialNumber, request.NoteDetail.InvoiceCorrelativeNumber);

        var debitNote = _documentGeneratorService.GenerateDebitNoteType(request, issuer);

        //Serialize to xml
        var xmlString = _sunatService.SerializeXmlDocument(typeof(DebitNoteType), debitNote);

        //Sign xml
        var xmlDoc = _sunatService.SignXml(xmlString, issuer, request.NoteDetail.DocumentType);

        //Zip xml
        var xmlFile = $"{issuer.IssuerId}-{request.NoteDetail.DocumentType}-{request.NoteDetail.Serie}{request.NoteDetail.SerialNumber.ToString("00")}-{request.NoteDetail.CorrelativeNumber.ToString("00000000")}.xml";
        var byteZippedXml = _sunatService.ZipXml(xmlDoc, Path.GetFileName(xmlFile));

        //Send bill
        var zippedFile = xmlFile.Replace(".xml", ".zip");
        var cdrByte = await _sunatService.SendBill("https://e-beta.sunat.gob.pe/ol-ti-itcpfegem-beta/billService",
                "20606022779MODDATOS",
                "moddatos",
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

    public async Task<InvoiceResponse> GetDebitNoteAsync(Guid id, bool trackChanges)
    {
        var debitNote = await GetInvoiceAndCheckIfItExists(id, trackChanges);

        var debitNoteResponse = _mapper.Map<Entities.Models.Invoice, InvoiceResponse>(debitNote);
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