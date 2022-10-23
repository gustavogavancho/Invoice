using Invoice.Shared.Request;
using Invoice.Shared.Response;

namespace Invoice.Service.Contracts.BusinessServices;

public interface IInvoiceService
{
    Task<InvoiceResponse> SendInvoiceType(Guid id, InvoiceRequest request, bool trackChanges);
    Task<InvoiceResponse> GetInvoiceAsync(Guid id, bool trackChanges);
}