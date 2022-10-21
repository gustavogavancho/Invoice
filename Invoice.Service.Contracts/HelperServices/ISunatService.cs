using Invoice.Entities.Models;
using System.Xml;

namespace Invoice.Service.Contracts.HelperServices;

public interface ISunatService
{
    string SerializeXmlDocument(Type documentType, object document);
    XmlDocument SignXml(string xml, Issuer issuer, string documentType);
    byte[] ZipXml(XmlDocument xmlDoc, string name);
    Task<byte[]> SendBill(string uri, string username, string password, string fileName, byte[] byteFile, string cdrFile);
    string[] ReadResponse(byte[] cdrByte);
}