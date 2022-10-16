using Invoice.Shared.Request;

namespace Invoice.Service.Contracts;

public interface IInvoiceService
{
    void SendInvoiceType(InvoiceRequest request);
}