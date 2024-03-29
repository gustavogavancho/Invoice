﻿using Invoice.Contracts.Repositories;

namespace Invoice.Repository.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly InvoiceContext _invoiceContext;
    private readonly Lazy<IIssuerRepository> _issuerRepository;
    private readonly Lazy<IInvoiceRepository> _invoiceRepository;
    private readonly Lazy<ITicketRepository> _ticketRepository;
    private readonly Lazy<IDespatchRepository> _despatchRepository;

    public RepositoryManager(InvoiceContext repositoryContext)
    {
        _invoiceContext = repositoryContext;
        _issuerRepository = new Lazy<IIssuerRepository>(() => new IssuerRepository(repositoryContext));
        _invoiceRepository = new Lazy<IInvoiceRepository>(() => new InvoiceRepository(repositoryContext));
        _ticketRepository = new Lazy<ITicketRepository>(() => new TicketRepository(repositoryContext));
        _despatchRepository = new Lazy<IDespatchRepository>(() => new DespatchRepository(repositoryContext));
    }
    public IIssuerRepository Issuer => _issuerRepository.Value;
    public IInvoiceRepository Invoice => _invoiceRepository.Value;
    public ITicketRepository Ticket => _ticketRepository.Value;
    public IDespatchRepository Despatch => _despatchRepository.Value;

    public async Task SaveAsync() => await _invoiceContext.SaveChangesAsync();
}