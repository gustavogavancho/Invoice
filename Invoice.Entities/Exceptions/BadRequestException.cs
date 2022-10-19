namespace Invoice.Entities.Exceptions;

[Serializable]
public abstract class BadRequestException : Exception
{
    protected BadRequestException(string message) : base(message)
    {
    }
}