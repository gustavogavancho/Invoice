namespace Invoice.Entities.Exceptions;

public sealed class ZipperException : Exception
{
    public ZipperException(string message) : base(message) { }
}