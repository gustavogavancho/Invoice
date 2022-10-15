using Invoice.Service.Contracts;
using Invoice.Shared;
using Invoice.Shared.Request;
using System.Xml;
using System.Xml.Serialization;
using UBLSunatPE;

namespace Invoice.Service;

public class InvoiceSerializerService : IInvoiceSerializerService
{
    public void SerializeInvoiceType(InvoiceRequest request)
    {
        var invoice = new InvoiceType
        {
            #region Headers

            Cac = SD.XmlnsCac,
            Cbc = SD.XmlnsCbc,
            Ccts = SD.XmlnsCcts,
            Ds = SD.XmlnsDs,
            Ext = SD.XmlnsExt,
            Qdt = SD.XmlnsQdt,
            Udt = SD.XmlnsUdt,

            #endregion

            #region Ubl and Schema

            UBLVersionID = new UBLVersionIDType { Value = request.UblSchemeRequest.UblVersionId },
            CustomizationID = new CustomizationIDType { Value = request.UblSchemeRequest.CustomizationId },

            #endregion
        };

        //Serialize docuemnt to XML
        SerializeXmlDocument("InvoiceType.xml", Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + $"\\XML", typeof(InvoiceType), invoice);
    }

    private static void SerializeXmlDocument(string fileName, string path, Type documentType, object document)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        using var xmlWriter = XmlWriter.Create($"{path}\\{fileName}", new XmlWriterSettings
        {
            Indent = true,
            IndentChars = "\t",
        });

        try
        {
            var xmlSerialized = new XmlSerializer(documentType);

            xmlSerialized.Serialize(xmlWriter, document);
        }
        finally
        {
            xmlWriter.Close();
        }
    }
}