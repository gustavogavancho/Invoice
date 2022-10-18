using Invoice.Shared.Request;

namespace Invoice.Service.Contracts;

public interface IInvoiceService
{
    Task SendInvoiceType(Guid id, InvoiceRequest request);
}