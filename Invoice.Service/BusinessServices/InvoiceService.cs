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

public class InvoiceService : IInvoiceService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IDocumentGeneratorService _documentGeneratorService;
    private readonly ISunatService _sunatService;

    public InvoiceService(IRepositoryManager repository, 
        ILoggerManager logger, 
        IMapper mapper,
        IDocumentGeneratorService documentGeneratorService,
        ISunatService sunatService)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
        _documentGeneratorService = documentGeneratorService;
        _sunatService = sunatService;
    }

    public async Task<InvoiceResponse> CreateInvoiceAsync(Guid id, InvoiceRequest request, bool trackChanges)
    {
        var issuer = await GetIssuerAndCheckIfItExists(id, trackChanges);

        var invoice = _documentGeneratorService.GenerateInvoiceType(request, issuer);

        //Serialize to xml
        var xmlString = _sunatService.SerializeXmlDocument(typeof(InvoiceType), invoice);

        //Sign xml
        var xmlDoc = _sunatService.SignXml(xmlString, issuer, request.InvoiceDetail.DocumentType);

        //Zip xml
        var xmlFile = $"{issuer.IssuerId}-{request.InvoiceDetail.DocumentType}-{request.InvoiceDetail.Serie}{request.InvoiceDetail.SerialNumber.ToString("00")}-{request.InvoiceDetail.CorrelativeNumber.ToString("00000000")}.xml";
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

        //Save invoice
        if (responses.Any(x => x.Contains("aceptada")))
        {
            var invoiceDb = _mapper.Map<InvoiceRequest, Invoice.Entities.Models.Invoice>(request);
            invoiceDb.IssuerId = issuer.Id;
            invoiceDb.InvoiceXml = xmlDoc.OuterXml;
            invoiceDb.Accepted = true;
            invoiceDb.SunatResponse = cdrByte;
            invoiceDb.Observations = string.Join("|", responses);
            _repository.Invoice.CreateInvoice(invoiceDb);
            await _repository.SaveAsync();

            var invoiceResponse = _mapper.Map<Entities.Models.Invoice, InvoiceResponse>(invoiceDb);
            return invoiceResponse;
        }
        return null;
    }

    public async Task<List<InvoiceResponse>> GetInvoicesAsync(bool trackChanges)
    {
        var invoices = await _repository.Invoice.GetInvoicesAsync(trackChanges);

        var invoicesResponse = _mapper.Map<IEnumerable<Entities.Models.Invoice>, List<InvoiceResponse>>(invoices);

        return invoicesResponse;
    }

    public async Task<InvoiceResponse> GetInvoiceBySerieAsync(string serie, uint serialNumber, uint correlativeNumber, bool trackChanges)
    {
        var invoice = await GetInvoiceBySerieAndCheckIfItExists(serie, serialNumber, correlativeNumber, trackChanges);

        var invoiceResponse = _mapper.Map<Entities.Models.Invoice, InvoiceResponse>(invoice);
        return invoiceResponse;
    }

    private async Task<Issuer> GetIssuerAndCheckIfItExists(Guid id, bool trackChanges)
    {
        var issuer = await _repository.Issuer.GetIssuerAsync(id, trackChanges);

        if (issuer is null)
            throw new IssuerNotFoundException(id);

        return issuer;
    }

    private async Task<Entities.Models.Invoice> GetInvoiceBySerieAndCheckIfItExists(string serie, uint serialNumber, uint correlativeNumber, bool trackChanges)
    {
        var invoice = await _repository.Invoice.GetInvoiceBySerieAsync(serie, serialNumber, correlativeNumber, trackChanges);

        if (invoice is null)
            throw new InvoiceNotFoundException(serie, serialNumber, correlativeNumber);

        return invoice;
    }
}