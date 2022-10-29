namespace Invoice.Contracts.Repositories;

public interface IRepositoryManager
{
    IIssuerRepository Issuer { get; }
    IInvoiceRepository Invoice { get; }
    ITicketRepository Ticket { get; }
    Task SaveAsync();
}