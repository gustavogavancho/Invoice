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

    public InvoiceService(IRepositoryManager repository, 
        ILoggerManager logger, 
        IMapper mapper,
        IDocumentGeneratorService documentGeneratorService,
        ISerializeXmlService serializeXmlService,
        ISignerService signerService,
        IZipperService zipperService)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
        _documentGeneratorService = documentGeneratorService;
        _serializeXmlService = serializeXmlService;
        _signerService = signerService;
        _zipperService = zipperService;
    }

    public async Task SendInvoiceType(Guid id, InvoiceRequest request, bool trackChanges)
    {
        var issuer = await GetIssuerAndCheckIfItExists(id, trackChanges);

        var invoice = _documentGeneratorService.GenerateInvoiceType(request, issuer);

        var fileName = $"{issuer.IssuerId}-{request.InvoiceDetail.DocumentType}-{request.InvoiceDetail.Serie}{request.InvoiceDetail.SerialNumber.ToString("00")}-{request.InvoiceDetail.CorrelativeNumber.ToString("00000000")}.xml";
        var path = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + "\\XML";

        _serializeXmlService.SerializeXmlDocument(fileName, path, typeof(InvoiceType), invoice);

        _signerService.SignXml(id, Path.Combine(path, fileName), issuer);

        _zipperService.ZipXml(Path.Combine(path, fileName));
    }

    private async Task<Issuer> GetIssuerAndCheckIfItExists(Guid id, bool trackChanges)
    {
        var issuer = await _repository.Issuer.GetIssuerAsync(id, trackChanges);

        if (issuer is null)
            throw new IssuerNotFoundException(id);

        return issuer;
    }
}