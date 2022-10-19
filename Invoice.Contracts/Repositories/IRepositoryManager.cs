namespace Invoice.Contracts.Repositories;

public interface IRepositoryManager
{
    IIssuerRepository Issuer { get; }
    Task SaveAsync();
}