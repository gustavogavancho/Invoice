namespace Invoice.Entities.Exceptions;

[Serializable]
public sealed class ReadResponseException : Exception
{
    public ReadResponseException(string message) : base($" Unprocesable response: \n{message}") { }
}