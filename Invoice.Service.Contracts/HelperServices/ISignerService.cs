namespace Invoice.Service.Contracts.HelperServices;

public interface ISignerService
{
    Task SignXml(Guid id, string file);
}