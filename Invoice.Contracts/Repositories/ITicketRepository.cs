using Invoice.Entities.Models;

namespace Invoice.Contracts.Repositories;

public interface ITicketRepository
{
    void CreateTicket(Ticket ticket);
    void DeleteTicket(Ticket ticket);
    Task<Ticket> GetTicketAsync(string ticketNumber, bool trackChanges);
    Task<IEnumerable<Ticket>> GetTicketsAsync(bool trackChanges);
}