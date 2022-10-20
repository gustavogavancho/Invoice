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

    public InvoiceService(IRepositoryManager repository, 
        ILoggerManager logger, 
        IMapper mapper,
        IDocumentGeneratorService documentGeneratorService,
        ISerializeXmlService serializeXmlService,
        ISignerService signerService,
        IZipperService zipperService,
        ISunatService sunatService)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
        _documentGeneratorService = documentGeneratorService;
        _serializeXmlService = serializeXmlService;
        _signerService = signerService;
        _zipperService = zipperService;
        _sunatService = sunatService;
    }

    public async Task SendInvoiceType(Guid id, InvoiceRequest request, bool trackChanges)
    {
        var issuer = await GetIssuerAndCheckIfItExists(id, trackChanges);

        var invoice = _documentGeneratorService.GenerateInvoiceType(request, issuer);

        var fileName = $"{issuer.IssuerId}-{request.InvoiceDetail.DocumentType}-{request.InvoiceDetail.Serie}{request.InvoiceDetail.SerialNumber.ToString("00")}-{request.InvoiceDetail.CorrelativeNumber.ToString("00000000")}.xml";
        var path = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + "\\XML";

        //Serialize to xml
        _serializeXmlService.SerializeXmlDocument(fileName, path, typeof(InvoiceType), invoice);

        //Sign xml
        _signerService.SignXml(id, Path.Combine(path, fileName), issuer);

        //Zip xml
        var fileZipped = _zipperService.ZipXml(Path.Combine(path, fileName));

        //Send bill
        string respuestArchivoZip = $"R-{issuer.IssuerId}-{request.InvoiceDetail.DocumentType}-{request.InvoiceDetail.Serie}{request.InvoiceDetail.SerialNumber.ToString("00")}-{request.InvoiceDetail.CorrelativeNumber.ToString("00000000")}.zip";
        byte[] bitArray = await File.ReadAllBytesAsync(fileZipped);

        string pathCdr = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + $"\\XMLCDR";

        if (!Directory.Exists(pathCdr))
        {
            Directory.CreateDirectory(pathCdr);
        }

        var result = await _sunatService.SendBill("https://e-beta.sunat.gob.pe/ol-ti-itcpfegem-beta/billService",
                "20606022779MODDATOS",
                "moddatos",
                Path.GetFileName(fileZipped),
                bitArray);

        using FileStream fs = new FileStream($"{pathCdr}\\{respuestArchivoZip}", FileMode.Create);
        fs.Write(result, 0, result.Length);
        fs.Close();
    }

    private async Task<Issuer> GetIssuerAndCheckIfItExists(Guid id, bool trackChanges)
    {
        var issuer = await _repository.Issuer.GetIssuerAsync(id, trackChanges);

        if (issuer is null)
            throw new IssuerNotFoundException(id);

        return issuer;
    }
}