﻿using Invoice.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Invoice.Repository.Repositories;

public class InvoiceRepository : RepositoryBase<Entities.Models.Invoice>, IInvoiceRepository
{
    public InvoiceRepository(InvoiceContext invoiceContext) : base(invoiceContext)
    {
    }

    public void CreateInvoice(Entities.Models.Invoice invoice) => Create(invoice);

    public void DeleteInvoice(Entities.Models.Invoice invoice) => Delete(invoice);

    public async Task<Entities.Models.Invoice> GetInvoiceAsync(Guid id, bool trackChanges) =>
        await FindByCondition(x => x.Id.Equals(id), trackChanges)
        .FirstOrDefaultAsync();

    public async Task<Entities.Models.Invoice> GetInvoiceBySerieAsync(string serie, uint serialNumber, uint correlativeNumber, bool trackChanges) =>
        await FindByCondition(x => x.InvoiceDetail.Serie == serie && x.InvoiceDetail.SerialNumber == serialNumber && x.InvoiceDetail.CorrelativeNumber == correlativeNumber, trackChanges)
        .FirstOrDefaultAsync();

    public async Task<IEnumerable<Entities.Models.Invoice>> GetTicketsByIssueDateAsync(DateTime issueDate, bool? summaryStatus, bool trackChanges) =>
        await FindByCondition(x => x.IssueDate.Date == issueDate.Date && 
        x.InvoiceDetail.DocumentType == "03" &&
        x.SummaryDocumentStatus == summaryStatus, trackChanges)
        .Include(x => x.InvoiceDetail)
        .Include(x => x.Receiver)
        .Include(x => x.TaxSubTotals)
        .ToListAsync();

    public async Task<IEnumerable<Entities.Models.Invoice>> GetInvoicesAsync(bool trackChanges) =>
        await FindAll(trackChanges)
        .ToListAsync();
}