using Invoice.Shared.Request;
using Invoice.Shared.Response;

namespace Invoice.Service.Contracts.BusinessServices;

public interface IDebitNoteService
{
    Task<InvoiceResponse> CreateDebitNoteAsync(Guid id, NoteRequest request, bool trackChanges);
    Task<InvoiceResponse> GetDebitNoteAsync(Guid id, bool trackChanges);
}