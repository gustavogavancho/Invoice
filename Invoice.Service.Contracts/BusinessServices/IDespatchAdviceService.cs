using Invoice.Shared.Request;
using Invoice.Shared.Response;

namespace Invoice.Service.Contracts.BusinessServices;

public interface IDespatchAdviceService
{
    Task<DespatchResponse> CreateDespatchAdviceAsync(Guid id, DespatchRequest request, bool trackChanges);
    Task<List<DespatchResponse>> GetDespatchesAsync(bool trackChanges);
    Task<DespatchResponse> GetDespatchAdviceBySerieAsync(string serie, uint serialNumber, uint correlativeNumber, bool trackChanges);
}