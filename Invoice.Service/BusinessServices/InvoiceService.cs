using Invoice.Service.Contracts.BusinessServices;
using Invoice.Service.Contracts.HelperServices;
using Invoice.Shared.Request;
using UBLSunatPE;

namespace Invoice.Service.BusinessServices;

public class InvoiceService : IInvoiceService
{
    private readonly IDocumentGeneratorService _documentGeneratorService;
    private readonly ISerializeXmlService _serializeXmlService;
    private readonly ISignerService _signerService;
    private readonly IZipperService _zipperService;

    public InvoiceService(IDocumentGeneratorService documentGeneratorService,
        ISerializeXmlService serializeXmlService,
        ISignerService signerService,
        IZipperService zipperService)
    {
        _documentGeneratorService = documentGeneratorService;
        _serializeXmlService = serializeXmlService;
        _signerService = signerService;
        _zipperService = zipperService;
    }

    public async Task SendInvoiceType(Guid id, InvoiceRequest request)
    {
        var invoice = _documentGeneratorService.GenerateInvoiceType(request);

        var fileName = $"{request.Issuer.IssuerId}-{request.InvoiceData.DocumentType}-{request.InvoiceData.Serie}{request.InvoiceData.SerialNumber.ToString("00")}-{request.InvoiceData.CorrelativeNumber.ToString("00000000")}.xml";
        var path = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + "\\XML";

        _serializeXmlService.SerializeXmlDocument(fileName, path, typeof(InvoiceType), invoice);
        await _signerService.SignXml(id, Path.Combine(path, fileName));
        _zipperService.ZipXml(Path.Combine(path, fileName));
    }
}