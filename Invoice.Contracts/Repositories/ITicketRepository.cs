using Invoice.Entities.Models;

namespace Invoice.Contracts.Repositories;

public interface ITicketRepository
{
    void CreateTicket(Ticket ticket);
    void DeleteTicket(Ticket ticket);
    Task<Ticket> GetTicketAsync(Guid id, bool trackChanges);
    Task<IEnumerable<Ticket>> GetTicketsAsync(bool trackChanges);
}