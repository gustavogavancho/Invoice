namespace Invoice.Entities.Exceptions;

[Serializable]
public sealed class IssuerNotFoundException : NotFoundException
{
    public IssuerNotFoundException(Guid id) : base($"The issuer with id: {id} doesn't exist in the database.")
    {
    }
}