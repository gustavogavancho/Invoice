namespace Invoice.Entities.Exceptions;

[Serializable]
public class DespatchNotFoundException : NotFoundException
{
    public DespatchNotFoundException(string serie, int serialNumber, int correlativeNumber) :
    base($"The Despatch advice with serie: {serie}{serialNumber.ToString("00")}-{correlativeNumber.ToString("00000000")} doesn't exist in the database.")
    {
    }
}