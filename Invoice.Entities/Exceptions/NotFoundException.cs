namespace Invoice.Entities.Exceptions;

[Serializable]
public abstract class NotFoundException : Exception
{
    protected NotFoundException(string message) : base(message) { }
}
