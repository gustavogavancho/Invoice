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
    private readonly Lazy<IDebitNoteService> _debitNoteService;

    public ServiceManager(IRepositoryManager repositoryManager,
        ILoggerManager logger,
        IMapper mapper,
        IDocumentGeneratorService documentGeneratorService,
        ISunatService sunatService)
    {
        _invoiceService = new Lazy<IInvoiceService>(() => new InvoiceService(repositoryManager, logger, mapper, documentGeneratorService, sunatService));
        _debitNoteService = new Lazy<IDebitNoteService>(() => new DebitNoteService(repositoryManager, logger, mapper, documentGeneratorService, sunatService));
        _issuerService = new Lazy<IIssuerService>(() => new IssuerService(repositoryManager, logger, mapper));
    }

    public IInvoiceService InvoiceService => _invoiceService.Value;
    public IIssuerService IssuerService => _issuerService.Value;
    public IDebitNoteService DebitNoteService => _debitNoteService.Value;
}