namespace Invoice.Entities.Exceptions;

[Serializable]
public class InvoiceNotFoundException : NotFoundException
{
    public InvoiceNotFoundException(string serie, uint serialNumber, uint correlativeNumber) :
        base($"The invoice with serie: {serie}{serialNumber.ToString("00")}-{correlativeNumber.ToString("00000000")} doesn't exist in the database.")
    {
    }

    public InvoiceNotFoundException(Guid id) : base($"The issuer with id: {id} doesn't exist in the database.")
    {
    }
}