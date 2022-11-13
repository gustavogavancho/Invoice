using Invoice.Shared.Request;
using Invoice.Shared.Response;

namespace Invoice.Service.Contracts.BusinessServices;

public interface IInvoiceService
{
    Task<InvoiceResponse> CreateInvoiceAsync(Guid id, InvoiceRequest request, bool trackChanges);
    Task<List<InvoiceResponse>> GetInvoicesAsync(bool trackChanges);
    Task<InvoiceResponse> GetInvoiceBySerieAsync(string serie, uint serialNumber, uint correlativeNumber, bool trackChanges);
}