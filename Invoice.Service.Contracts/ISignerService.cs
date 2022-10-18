namespace Invoice.Service.Contracts;

public interface ISignerService
{
    Task SignXml(Guid id, string file, string password);
}