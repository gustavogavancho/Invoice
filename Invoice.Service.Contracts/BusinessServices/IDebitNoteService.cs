using Invoice.Shared.Request;
using Invoice.Shared.Response;

namespace Invoice.Service.Contracts.BusinessServices;

public interface IDebitNoteService
{
    Task<DebitNoteResponse> CreateDebitNoteAsync(Guid id, DebitNoteRequest request, bool trackChanges);
}