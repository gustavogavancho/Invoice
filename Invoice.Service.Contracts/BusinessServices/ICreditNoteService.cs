using Invoice.Shared.Request;
using Invoice.Shared.Response;

namespace Invoice.Service.Contracts.BusinessServices;

public interface ICreditNoteService
{
    Task<CreditNoteResponse> CreateCreditNoteAsync(Guid id, CreditNoteRequest request, bool trackChanges);
    Task<CreditNoteResponse> GetCreditNoteAsync(Guid id, bool trackChanges);
}