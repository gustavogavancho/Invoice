using Invoice.Entities.Models;

namespace Invoice.Service.Contracts.HelperServices;

public interface ISignerService
{
    void SignXml(Guid id, string file, Issuer issuer);
}