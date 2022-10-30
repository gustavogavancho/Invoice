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

    public async Task<Ticket> GetTicketAsync(string ticketNumber, bool trackChanges) =>
        await FindByCondition(x => x.TicketNumber.Equals(ticketNumber), trackChanges)
        .FirstOrDefaultAsync();

    public async Task<IEnumerable<Ticket>> GetTicketsAsync(bool trackChanges) =>
        await FindAll(trackChanges)
        .ToListAsync();
}