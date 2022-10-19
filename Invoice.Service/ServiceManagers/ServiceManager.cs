using AutoMapper;
using Invoice.Contracts.Logger;
using Invoice.Contracts.Repositories;
using Invoice.Service.BusinessServices;
using Invoice.Service.Contracts.BusinessServices;
using Invoice.Service.Contracts.HelperServices;
using Invoice.Service.Contracts.ServiceManagers;

namespace Invoice.Service.ServiceManagers;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IInvoiceService> _invoiceService;
    private readonly Lazy<IIssuerService> _issuerService;

    public ServiceManager(IRepositoryManager repositoryManager,
        ILoggerManager logger,
        IMapper mapper,
        IDocumentGeneratorService documentGeneratorService,
        ISerializeXmlService serializeXmlService,
        ISignerService signerService,
        IZipperService zipperService)
    {
        _invoiceService = new Lazy<IInvoiceService>(() => new InvoiceService(repositoryManager, logger, mapper, documentGeneratorService, serializeXmlService, signerService, zipperService));
        _issuerService = new Lazy<IIssuerService>(() => new IssuerService(repositoryManager, logger, mapper));
    }

    public IInvoiceService InvoiceService => _invoiceService.Value;
    public IIssuerService IssuerService => _issuerService.Value;
}