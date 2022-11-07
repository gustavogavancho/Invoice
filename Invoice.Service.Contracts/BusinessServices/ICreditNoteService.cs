using Invoice.Shared.Request;
using Invoice.Shared.Response;

namespace Invoice.Service.Contracts.BusinessServices;

public interface ICreditNoteService
{
    Task<InvoiceResponse> CreateCreditNoteAsync(Guid id, CreditNoteRequest request, bool trackChanges);
    Task<InvoiceResponse> GetCreditNoteAsync(Guid id, bool trackChanges);
}