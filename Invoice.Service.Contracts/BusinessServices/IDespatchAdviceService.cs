using Invoice.Shared.Request;
using Invoice.Shared.Response;

namespace Invoice.Service.Contracts.BusinessServices;

public interface IDespatchAdviceService
{
    Task<InvoiceResponse> CreateDespatchAdviceAsync(Guid id, DespatchAdviceRequest request, bool trackChanges);
    Task<InvoiceResponse> GetDespatchAdviceAsync(Guid id, bool trackChanges);
}