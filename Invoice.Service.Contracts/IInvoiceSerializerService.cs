using Invoice.Shared.Request;

namespace Invoice.Service.Contracts;

public interface IInvoiceSerializerService
{
    void SerializeInvoiceType(InvoiceRequest request);
}