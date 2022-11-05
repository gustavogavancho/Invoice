using Invoice.Shared.Request;
using Invoice.Shared.Response;

namespace Invoice.Service.Contracts.BusinessServices;

public interface IDespatchAdviceService
{
    Task<DespatchAdviceResponse> CreateDespatchAdviceAsync(Guid id, DespatchAdviceRequest request, bool trackChanges);
    Task<DespatchAdviceResponse> GetDespatchAdviceAsync(Guid id, bool trackChanges);
}