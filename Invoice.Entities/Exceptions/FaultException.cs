namespace Invoice.Entities.Exceptions
{
    [Serializable]
    public sealed class FaultException : BadRequestException
    {
        public FaultException(string message) : base($" Unprocesable request: \n{message}") { }
    }
}
