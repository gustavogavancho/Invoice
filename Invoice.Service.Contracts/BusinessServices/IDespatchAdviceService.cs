using Invoice.Shared.Request;
using Invoice.Shared.Response;

namespace Invoice.Service.Contracts.BusinessServices;

public interface IDespatchAdviceService
{
    Task<DespatchResponse> CreateDespatchAdviceAsync(Guid id, DespatchRequest request, bool trackChanges);
    Task<DespatchResponse> GetDespatchAdviceAsync(Guid id, bool trackChanges);
}