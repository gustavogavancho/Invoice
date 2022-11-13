using Invoice.Contracts.Repositories;
using Invoice.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Invoice.Repository.Repositories;

public class DespatchRepository : RepositoryBase<Despatch>, IDespatchRepository
{
    public DespatchRepository(InvoiceContext invoiceContext) : base(invoiceContext)
    {
    }

    public void CreateDespatch(Despatch despatch) => Create(despatch);
    public void DeleteDespatch(Despatch despatch) => Delete(despatch);

    public async Task<Despatch> GetDespatchAsync(Guid id, bool trackChanges) =>
        await FindByCondition(x => x.Id.Equals(id), trackChanges)
        .FirstOrDefaultAsync();

    public async Task<IEnumerable<Despatch>> GetDespatchesAsync(bool trackChanges) =>
        await FindAll(trackChanges)
        .ToListAsync();
}