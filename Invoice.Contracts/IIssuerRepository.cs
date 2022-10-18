using Invoice.Entities;

namespace Invoice.Contracts;

public interface IIssuerRepository
{
    Task CreateIssuer(Issuer issuer);
    Task<Issuer> GetIssuer(Guid id);
    Task<List<Issuer>> GetIssuers();
    Task UpdateIssuer(Guid id, Issuer issuer);
    Task DeleteIssuer(Guid id);
}