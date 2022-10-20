namespace Invoice.Service.Contracts.HelperServices;

public interface ISunatService
{
    Task<byte[]> SendBill(string uri, string username, string password, string fileName, byte[] file);
}