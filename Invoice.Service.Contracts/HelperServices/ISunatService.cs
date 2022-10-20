namespace Invoice.Service.Contracts.HelperServices;

public interface ISunatService
{
    Task SendBill(string uri, string username, string password, string fileName, string file, string cdrFile);
}