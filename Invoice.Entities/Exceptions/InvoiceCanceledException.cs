namespace Invoice.Entities.Exceptions;

[Serializable]
public sealed class InvoiceCanceledException : BadRequestException
{
    public InvoiceCanceledException(string serie, uint serialNumber, uint correlativeNumber) :
        base($"The invoice with serie: {serie}{serialNumber.ToString("00")}-{correlativeNumber.ToString("00000000")} is already canceled.")
    {
    }
}