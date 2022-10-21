namespace Invoice.Entities.Exceptions;

[Serializable]
public sealed class SunatException : Exception
{
    public SunatException(string message) : base($"Something went wrong: {message}") { }
}