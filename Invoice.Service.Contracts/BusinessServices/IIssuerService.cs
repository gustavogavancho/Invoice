using Invoice.Shared.Request;
using Invoice.Shared.Response;

namespace Invoice.Service.Contracts.BusinessServices;

public interface IIssuerService
{
    Task CreateIssuer(IssuerRequest issuerRequest);
    Task DeleteIssuer(Guid id);
    Task<IssuerResponse> GetIssuer(Guid guid);
    Task<List<IssuerResponse>> GetIssuers();
    Task UpdateIssuer(Guid id, IssuerRequest issuerRequest);
}