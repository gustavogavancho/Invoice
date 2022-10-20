using AutoMapper;
using Invoice.Contracts.Logger;
using Invoice.Contracts.Repositories;
using Invoice.Entities.Exceptions;
using Invoice.Entities.Models;
using Invoice.Service.Contracts.BusinessServices;
using Invoice.Service.Contracts.HelperServices;
using Invoice.Shared.Request;
using UBLSunatPE;

namespace Invoice.Service.BusinessServices;

public class InvoiceService : IInvoiceService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IDocumentGeneratorService _documentGeneratorService;
    private readonly ISerializeXmlService _serializeXmlService;
    private readonly ISignerService _signerService;
    private readonly IZipperService _zipperService;
    private readonly ISunatService _sunatService;
    private readonly IReadResponseService _readResponseService;

    public InvoiceService(IRepositoryManager repository, 
        ILoggerManager logger, 
        IMapper mapper,
        IDocumentGeneratorService documentGeneratorService,
        ISerializeXmlService serializeXmlService,
        ISignerService signerService,
        IZipperService zipperService,
        ISunatService sunatService,
        IReadResponseService readResponseService)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
        _documentGeneratorService = documentGeneratorService;
        _serializeXmlService = serializeXmlService;
        _signerService = signerService;
        _zipperService = zipperService;
        _sunatService = sunatService;
        _readResponseService = readResponseService;
    }

    public async Task SendInvoiceType(Guid id, InvoiceRequest request, bool trackChanges)
    {
        var issuer = await GetIssuerAndCheckIfItExists(id, trackChanges);

        var invoice = _documentGeneratorService.GenerateInvoiceType(request, issuer);

        //Serialize to xml
        var xmlFile = GetDocumentName(issuer, request, typeof(InvoiceRequest), "xml", "XML", false);
        _serializeXmlService.SerializeXmlDocument(xmlFile, typeof(InvoiceType), invoice);

        //Sign xml
        _signerService.SignXml(id, xmlFile, issuer);

        //Zip xml
        var zippedFile = GetDocumentName(issuer, request, typeof(InvoiceRequest), "zip", "ZIPPED", false);
        _zipperService.ZipXml(xmlFile, zippedFile);

        //Send bill
        var cdrFile = GetDocumentName(issuer, request, typeof(InvoiceRequest), "zip", "CDR", true);
        await _sunatService.SendBill("https://e-beta.sunat.gob.pe/ol-ti-itcpfegem-beta/billService",
                "20606022779MODDATOS",
                "moddatos",
                Path.GetFileName(zippedFile),
                zippedFile,
                cdrFile);

        //Read response
        var response = _readResponseService.ReadResponse(Path.Combine(cdrFile));
    }

    private async Task<Issuer> GetIssuerAndCheckIfItExists(Guid id, bool trackChanges)
    {
        var issuer = await _repository.Issuer.GetIssuerAsync(id, trackChanges);

        if (issuer is null)
            throw new IssuerNotFoundException(id);

        return issuer;
    }

    private static string GetDocumentName(Issuer issuer, object document, Type documentType, string extension, string path, bool response)
    {
        dynamic documenConverted = Convert.ChangeType(document, documentType);

        var fileName = $"{issuer.IssuerId}-{documenConverted.InvoiceDetail.DocumentType}-{documenConverted.InvoiceDetail.Serie}{documenConverted.InvoiceDetail.SerialNumber.ToString("00")}-{documenConverted.InvoiceDetail.CorrelativeNumber.ToString("00000000")}.{extension}";

        if (response)
            fileName = "R-" + fileName;

        var directory = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + $"\\{path}";

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        return Path.Combine(directory, fileName);
    }
}