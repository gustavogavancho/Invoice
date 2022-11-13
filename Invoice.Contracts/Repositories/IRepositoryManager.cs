namespace Invoice.Contracts.Repositories;

public interface IRepositoryManager
{
    IIssuerRepository Issuer { get; }
    IInvoiceRepository Invoice { get; }
    ITicketRepository Ticket { get; }
    IDespatchRepository Despatch { get; }
    Task SaveAsync();
}