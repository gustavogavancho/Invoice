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

public class DespatchAdviceService : IDespatchAdviceService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IDocumentGeneratorService _documentGeneratorService;
    private readonly ISunatService _sunatService;

    public DespatchAdviceService(IRepositoryManager repository,
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

    public async Task<InvoiceResponse> CreateDespatchAdviceAsync(Guid id, DespatchAdviceRequest request, bool trackChanges)
    {
        var issuer = await GetIssuerAndCheckIfItExists(id, trackChanges);

        var despatchAdvice = _documentGeneratorService.GenerateDespatchAdviceType(request, issuer);

        //Serialize to xml
        var xmlString = _sunatService.SerializeXmlDocument(typeof(DespatchAdviceType), despatchAdvice);

        //Sign xml
        var xmlDoc = _sunatService.SignXml(xmlString, issuer, request.DespatchAdviceDetail.DocumentType);

        //Zip xml
        var xmlFile = $"{issuer.IssuerId}-{request.DespatchAdviceDetail.DocumentType}-{request.DespatchAdviceDetail.Serie}{request.DespatchAdviceDetail.SerialNumber.ToString("00")}-{request.DespatchAdviceDetail.CorrelativeNumber.ToString("00000000")}.xml";
        var byteZippedXml = _sunatService.ZipXml(xmlDoc, Path.GetFileName(xmlFile));

        //Send bill
        var zippedFile = xmlFile.Replace(".xml", ".zip");
        var cdrByte = await _sunatService.SendBill("https://e-beta.sunat.gob.pe/ol-ti-itemision-guia-gem-beta/billService",
                "20606022779MODDATOS",
                "moddatos",
                zippedFile,
                byteZippedXml);

        //Read response
        var responses = _sunatService.ReadResponse(cdrByte);

        //Save invoice
        if (responses.Any(x => x.Contains("aceptado")))
        {
            var invoiceDb = _mapper.Map<DespatchAdviceRequest, Invoice.Entities.Models.Invoice>(request);
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

    public async Task<InvoiceResponse> GetDespatchAdviceAsync(Guid id, bool trackChanges)
    {
        throw new NotImplementedException();
    }

    private async Task<Issuer> GetIssuerAndCheckIfItExists(Guid id, bool trackChanges)
    {
        var issuer = await _repository.Issuer.GetIssuerAsync(id, trackChanges);

        if (issuer is null)
            throw new IssuerNotFoundException(id);

        return issuer;
    }
}