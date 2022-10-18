namespace Invoice.Service.Contracts.HelperServices;

public interface ISerializeXmlService
{
    void SerializeXmlDocument(string fileName, string path, Type documentType, object document);
}