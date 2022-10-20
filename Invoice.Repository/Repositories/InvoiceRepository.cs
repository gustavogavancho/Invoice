using Invoice.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Invoice.Repository.Repositories;

public class InvoiceRepository : RepositoryBase<Entities.Models.Invoice>, IInvoiceRepository
{
    public InvoiceRepository(InvoiceContext invoiceContext) : base(invoiceContext)
    {
    }

    public void CreateInvoice(Entities.Models.Invoice invoice) => Create(invoice);

    public void DeleteInvoice(Entities.Models.Invoice invoice) => Delete(invoice);

    public async Task<Entities.Models.Invoice> GetInvoiceAsync(Guid id, bool trackChanges) =>
        await FindByCondition(x => x.Id.Equals(id), trackChanges)
        .SingleOrDefaultAsync();

    public async Task<IEnumerable<Entities.Models.Invoice>> GetInvoiceAsync(bool trackChanges) =>
        await FindAll(trackChanges)
        .ToListAsync();
}