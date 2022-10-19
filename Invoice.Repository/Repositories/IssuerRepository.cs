using Invoice.Contracts.Repositories;
using Invoice.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Invoice.Repository.Repositories;

public class IssuerRepository : RepositoryBase<Issuer>, IIssuerRepository
{
    public IssuerRepository(InvoiceContext invoiceContext) : base(invoiceContext)
    {
    }

    public void CreateIssuer(Issuer issuer) => Create(issuer);

    public void DeleteIssuer(Issuer issuer) => Delete(issuer);

    public async Task<Issuer> GetIssuerAsync(Guid id, bool trackChanges) =>
        await FindByCondition(x => x.Id.Equals(id), trackChanges)
        .SingleOrDefaultAsync();

    public async Task<IEnumerable<Issuer>> GetIssuersAsync(bool trackChanges) =>
        await FindAll(trackChanges)
        .ToListAsync();
}