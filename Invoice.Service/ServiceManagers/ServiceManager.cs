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
    private readonly Lazy<ICreditNoteService> _creditNoteService;
    private readonly Lazy<ISummaryDocumentsService> _summaryDocumentsService;
    private readonly Lazy<ITicketService> _ticketService;
    private readonly Lazy<IVoidedDocumentsService> _voidedDocumentsService;

    public ServiceManager(IRepositoryManager repositoryManager,
        ILoggerManager logger,
        IMapper mapper,
        IDocumentGeneratorService documentGeneratorService,
        ISunatService sunatService)
    {
        _invoiceService = new Lazy<IInvoiceService>(() => new InvoiceService(repositoryManager, logger, mapper, documentGeneratorService, sunatService));
        _debitNoteService = new Lazy<IDebitNoteService>(() => new DebitNoteService(repositoryManager, logger, mapper, documentGeneratorService, sunatService));
        _creditNoteService = new Lazy<ICreditNoteService>(() => new CreditNoteService(repositoryManager, logger, mapper, documentGeneratorService, sunatService));
        _summaryDocumentsService = new Lazy<ISummaryDocumentsService>(() => new SummaryDocumentsService(repositoryManager, logger, mapper, documentGeneratorService, sunatService));
        _voidedDocumentsService = new Lazy<IVoidedDocumentsService>(() => new VoidedDocumentsService(repositoryManager, logger, mapper, documentGeneratorService, sunatService));
        _ticketService = new Lazy<ITicketService>(() => new TicketService(repositoryManager, logger, mapper, documentGeneratorService, sunatService));
        _issuerService = new Lazy<IIssuerService>(() => new IssuerService(repositoryManager, logger, mapper));
    }

    public IInvoiceService InvoiceService => _invoiceService.Value;
    public IIssuerService IssuerService => _issuerService.Value;
    public IDebitNoteService DebitNoteService => _debitNoteService.Value;
    public ICreditNoteService CreditNoteService => _creditNoteService.Value;
    public ITicketService TicketService => _ticketService.Value;
    public ISummaryDocumentsService SummaryDocumentsService => _summaryDocumentsService.Value;
    public IVoidedDocumentsService VoidedDocumentsService => _voidedDocumentsService.Value;
}