namespace Invoice.Entities.Exceptions;

public sealed class SignerException : Exception
{
    public SignerException(string message) : base(message) { }
}