namespace Invoice.Entities.Exceptions;

[Serializable]
public sealed class SerializeXmlException : Exception
{
    public SerializeXmlException(string message) : base(message) { }
}