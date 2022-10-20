namespace Invoice.Contracts.Repositories;

public interface IInvoiceRepository
{
    void CreateInvoice(Entities.Models.Invoice invoice);
    void DeleteInvoice(Entities.Models.Invoice invoice);
    Task<Entities.Models.Invoice> GetInvoiceAsync(Guid id, bool trackChanges);
    Task<IEnumerable<Entities.Models.Invoice>> GetInvoiceAsync(bool trackChanges);
}
