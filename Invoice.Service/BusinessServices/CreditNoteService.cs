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

public class CreditNoteService : ICreditNoteService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IDocumentGeneratorService _documentGeneratorService;
    private readonly ISunatService _sunatService;

    public CreditNoteService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IDocumentGeneratorService documentGeneratorService, ISunatService sunatService)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
        _documentGeneratorService = documentGeneratorService;
        _sunatService = sunatService;
    }

    public async Task<CreditNoteResponse> CreateCreditNoteAsync(Guid id, CreditNoteRequest request, bool trackChanges)
    {
        var issuer = await GetIssuerAndCheckIfItExists(id, trackChanges);

        var invoice = await GetInvoiceBySerieAndCheckIfItExists(request.CreditNoteDetail.InvoiceSerie, request.CreditNoteDetail.InvoiceSerialNumber, request.CreditNoteDetail.InvoiceCorrelativeNumber, true);

        if (invoice.Canceled)
            throw new InvoiceCanceledException(request.CreditNoteDetail.InvoiceSerie, request.CreditNoteDetail.InvoiceSerialNumber, request.CreditNoteDetail.InvoiceCorrelativeNumber);

        var creditNote = _documentGeneratorService.GenerateCreditNoteType(request, issuer);

        //Serialize to xml
        var xmlString = _sunatService.SerializeXmlDocument(typeof(CreditNoteType), creditNote);

        //Sign xml
        var xmlDoc = _sunatService.SignXml(xmlString, issuer, request.CreditNoteDetail.DocumentType);

        //Zip xml
        var xmlFile = $"{issuer.IssuerId}-{request.CreditNoteDetail.DocumentType}-{request.CreditNoteDetail.Serie}{request.CreditNoteDetail.SerialNumber.ToString("00")}-{request.CreditNoteDetail.CorrelativeNumber.ToString("00000000")}.xml";
        var byteZippedXml = _sunatService.ZipXml(xmlDoc, Path.GetFileName(xmlFile));

        //Send bill
        var zippedFile = xmlFile.Replace(".xml", ".zip");
        var cdrFile = "R-" + zippedFile;
        var cdrByte = await _sunatService.SendBill("https://e-beta.sunat.gob.pe/ol-ti-itcpfegem-beta/billService",
                "20606022779MODDATOS",
                "moddatos",
                zippedFile,
                byteZippedXml,
                cdrFile);

        //Read response
        var responses = _sunatService.ReadResponse(cdrByte);

        //Save debit note
        if (responses.Any(x => x.Contains("aceptada")))
        {
            var invoiceDb = _mapper.Map<CreditNoteRequest, Invoice.Entities.Models.Invoice>(request);
            invoiceDb.IssuerId = issuer.Id;
            invoiceDb.InvoiceXml = xmlDoc.OuterXml;
            invoiceDb.Accepted = true;
            invoiceDb.SunatResponse = cdrByte;
            invoiceDb.Observations = string.Join("|", responses);
            _repository.Invoice.CreateInvoice(invoiceDb);
            invoice.Canceled = true;
            invoice.CanceledReason = responses.FirstOrDefault(x => x.Contains("aceptada"));
            await _repository.SaveAsync();

            var invoiceResponse = _mapper.Map<Entities.Models.Invoice, CreditNoteResponse>(invoiceDb);
            return invoiceResponse;
        }

        return null;
    }

    public async Task<CreditNoteResponse> GetCreditNoteAsync(Guid id, bool trackChanges)
    {
        var creditNote = await GetInvoiceAndCheckIfItExists(id, trackChanges);

        var debitNoteResponse = _mapper.Map<Entities.Models.Invoice, CreditNoteResponse>(creditNote);
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