using Invoice.Shared.Request;
using Invoice.Shared.Response;

namespace Invoice.Service.Contracts.BusinessServices;

public interface IInvoiceService
{
    Task<DebitNoteResponse> CreateInvoiceAsync(Guid id, InvoiceRequest request, bool trackChanges);
    Task<DebitNoteResponse> CreateDebitNoteAsync(Guid id, DebitNoteRequest request, bool trackChanges);
    Task<DebitNoteResponse> GetInvoiceAsync(Guid id, bool trackChanges);
}