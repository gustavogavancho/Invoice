using Invoice.Service.Contracts.BusinessServices;

namespace Invoice.Service.Contracts.ServiceManagers;

public interface IServiceManager
{
    IInvoiceService InvoiceService { get; }
    IIssuerService IssuerService { get; }
    IDebitNoteService DebitNoteService { get; }
    ICreditNoteService CreditNoteService { get; }
}