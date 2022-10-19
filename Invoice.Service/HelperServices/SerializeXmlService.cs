using System.Xml.Serialization;
using System.Xml;
using Invoice.Service.Contracts.HelperServices;
using Invoice.Entities.Exceptions;

namespace Invoice.Service.HelperServices;

public class SerializeXmlService : ISerializeXmlService
{
    public void SerializeXmlDocument(string fileName, string path, Type documentType, object document)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        using var xmlWriter = XmlWriter.Create(Path.Combine(path, fileName), new XmlWriterSettings
        {
            Indent = true,
            IndentChars = "\t",
        });

        try
        {
            var xmlSerialized = new XmlSerializer(documentType);

            xmlSerialized.Serialize(xmlWriter, document);
        }
        catch(Exception ex)
        {
            throw new SerializeXmlException(ex.Message);
        }
        finally
        {
            xmlWriter.Close();
        }
    }
}