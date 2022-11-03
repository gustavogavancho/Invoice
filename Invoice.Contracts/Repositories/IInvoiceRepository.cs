namespace Invoice.Contracts.Repositories;

public interface IInvoiceRepository
{
    void CreateInvoice(Entities.Models.Invoice invoice);
    void DeleteInvoice(Entities.Models.Invoice invoice);
    Task<Entities.Models.Invoice> GetInvoiceAsync(Guid id, bool trackChanges);
    Task<Entities.Models.Invoice> GetInvoiceBySerieAsync(string serie, uint serialNumber, uint correlativeNumber, bool trackChanges);
    Task<IEnumerable<Entities.Models.Invoice>> GetTicketsByIssueDateAsync(DateTime issueDate, bool? summaryStatus, bool trackChanges);
    Task<IEnumerable<Entities.Models.Invoice>> GetInvoicesAsync(bool trackChanges);
}
