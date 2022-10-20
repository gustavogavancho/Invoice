namespace Invoice.Service.Contracts.HelperServices;

public interface ISerializeXmlService
{
    void SerializeXmlDocument(string xmlFile, Type documentType, object document);
}