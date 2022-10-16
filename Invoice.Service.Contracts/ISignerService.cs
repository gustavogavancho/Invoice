namespace Invoice.Service.Contracts;

public interface ISignerService
{
    void SignXml(string file, string password);
}