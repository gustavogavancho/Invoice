namespace Invoice.Service.Contracts;

public interface ISerializeXmlService
{
    void SerializeXmlDocument(string fileName, string path, Type documentType, object document);
}