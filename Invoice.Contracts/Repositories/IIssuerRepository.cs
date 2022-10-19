using Invoice.Entities.Models;

namespace Invoice.Contracts.Repositories;

public interface IIssuerRepository
{
    void CreateIssuer(Issuer issuer);
    void DeleteIssuer(Issuer issuer);
    Task<Issuer> GetIssuerAsync(Guid id, bool trackChanges);
    Task<IEnumerable<Issuer>> GetIssuersAsync(bool trackChanges);
}