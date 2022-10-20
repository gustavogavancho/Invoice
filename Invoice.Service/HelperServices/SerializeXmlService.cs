using System.Xml.Serialization;
using System.Xml;
using Invoice.Service.Contracts.HelperServices;
using Invoice.Entities.Exceptions;

namespace Invoice.Service.HelperServices;

public class SerializeXmlService : ISerializeXmlService
{
    public void SerializeXmlDocument(string xmlFile, Type documentType, object document)
    {
        using var xmlWriter = XmlWriter.Create(xmlFile, new XmlWriterSettings
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