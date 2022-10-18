using Invoice.Shared.Request;

namespace Invoice.Service.Contracts.BusinessServices;

public interface IInvoiceService
{
    Task SendInvoiceType(Guid id, InvoiceRequest request);
}