using Invoice.Contracts.Repositories;
using Invoice.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Invoice.Repository.Repositories;

public class TicketRepository : RepositoryBase<Ticket>, ITicketRepository
{
    public TicketRepository(InvoiceContext invoiceContext) : base(invoiceContext)
    {
    }

    public void CreateTicket(Ticket ticket) => Create(ticket);

    public void DeleteTicket(Ticket ticket) => Delete(ticket);

    public async Task<Ticket> GetTicketAsync(Guid id, bool trackChanges) =>
        await FindByCondition(x => x.Id.Equals(id), trackChanges)
        .SingleOrDefaultAsync();

    public async Task<IEnumerable<Ticket>> GetTicketsAsync(bool trackChanges) =>
        await FindAll(trackChanges)
        .ToListAsync();
}