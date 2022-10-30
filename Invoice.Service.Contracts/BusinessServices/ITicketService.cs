using Invoice.Shared.Response;

namespace Invoice.Service.Contracts.BusinessServices;

public interface ITicketService
{
    Task<TicketResponse> GetTicketAsync(string ticketNumber, bool trackChanges);
}