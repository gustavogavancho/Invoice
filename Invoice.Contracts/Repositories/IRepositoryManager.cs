namespace Invoice.Contracts.Repositories;

public interface IRepositoryManager
{
    IIssuerRepository Issuer { get; }
    IInvoiceRepository Invoice { get; }
    Task SaveAsync();
}