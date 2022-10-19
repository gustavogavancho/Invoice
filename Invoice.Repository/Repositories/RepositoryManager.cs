using Invoice.Contracts.Repositories;

namespace Invoice.Repository.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly InvoiceContext _invoiceContext;
    private readonly Lazy<IIssuerRepository> _issuerRepository;
    public RepositoryManager(InvoiceContext repositoryContext)
    {
        _invoiceContext = repositoryContext;
        _issuerRepository = new Lazy<IIssuerRepository>(() => new IssuerRepository(repositoryContext));
    }
    public IIssuerRepository Issuer => _issuerRepository.Value;

    public async Task SaveAsync() => await _invoiceContext.SaveChangesAsync();
}