using Invoice.Shared.Request;
using Invoice.Shared.Response;

namespace Invoice.Service.Contracts.BusinessServices;

public interface IIssuerService
{
    Task<IssuerResponse> CreateIssuerAsync(IssuerRequest issuerRequest);
    Task DeleteIssuerAsync(Guid id, bool trackChanges);
    Task<IssuerResponse> GetIssuerAsync(Guid id, bool trackChanges);
    Task<IEnumerable<IssuerResponse>> GetIssuersAsync(bool trackChanges);
    Task UpdateIssuerAsync(Guid id, IssuerRequest issuerRequest, bool trackChanges);
}