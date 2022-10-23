using Invoice.Service.Contracts.HelperServices;
using Invoice.Service.Helpers;
using ServiceSunat;
using System.Net;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.Xml;
using Invoice.Entities.Models;
using System.Xml.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.IO.Compression;
using Invoice.Entities.Exceptions;

namespace Invoice.Service.HelperServices;

public class SunatService : ISunatService
{
    public string SerializeXmlDocument(Type documentType, object document)
    {
        using var stringWriter = new StringWriter();

        try
        {
            new XmlSerializer(documentType).Serialize(stringWriter, document);

            return stringWriter.ToString();
        }
        catch (Exception ex)
        {
            throw new SunatException(ex.Message);
        }
        finally
        {
            stringWriter.Close();
        }
    }

    public XmlDocument SignXml(string xml, Issuer issuer, string documentType)
    {
        try
        {
            xml = xml.Replace(@"<ext:UBLExtension />", @"<ext:UBLExtension> <ext:ExtensionContent /></ext:UBLExtension>");
            xml = xml.Replace("xsi:type=", "");
            xml = xml.Replace("cbc:SerialIDType", "");
            xml = xml.Replace("\"\"", "");

            string l_xpath = "";

            X509Certificate2 MonCertificat = new X509Certificate2(issuer.BetaCertificate, issuer.BetaCertificatePasword);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.LoadXml(xml);
            SignedXml signedXml = new SignedXml(xmlDoc);
            signedXml.SigningKey = MonCertificat.GetRSAPrivateKey();
            KeyInfo KeyInfo = new KeyInfo();
            Reference Reference = new Reference();
            Reference.Uri = "";
            Reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
            signedXml.AddReference(Reference);
            X509Chain X509Chain = new X509Chain();
            X509Chain.Build(MonCertificat);
            X509ChainElement local_element = X509Chain.ChainElements[0];
            KeyInfoX509Data x509Data = new KeyInfoX509Data(local_element.Certificate);
            string subjectName = local_element.Certificate.Subject;
            x509Data.AddSubjectName(subjectName);
            KeyInfo.AddClause(x509Data);
            signedXml.KeyInfo = KeyInfo;
            signedXml.ComputeSignature();
            XmlElement signature = signedXml.GetXml();
            signature.Prefix = "ds";
            signedXml.ComputeSignature();
            foreach (XmlNode node in signature.SelectNodes("descendant-or-self::*[namespace-uri()='http://www.w3.org/2000/09/xmldsig#']"))
            {
                if (node.LocalName == "Signature")
                {
                    XmlAttribute newAttribute = xmlDoc.CreateAttribute("Id");
                    newAttribute.Value = "SignSUNAT";
                    node.Attributes.Append(newAttribute);
                }
            }
            XmlNamespaceManager nsMgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsMgr.AddNamespace("sac", "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1");
            nsMgr.AddNamespace("ccts", "urn:un:unece:uncefact:documentation:2");
            nsMgr.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");

            switch (documentType)
            {
                case "01":
                case "03"
               :
                    {
                        nsMgr.AddNamespace("tns", "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2");
                        l_xpath = "/tns:Invoice/ext:UBLExtensions/ext:UBLExtension[1]/ext:ExtensionContent";
                        break;
                    }

                case "07"
         :
                    {
                        nsMgr.AddNamespace("tns", "urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2");
                        l_xpath = "/tns:CreditNote/ext:UBLExtensions/ext:UBLExtension[1]/ext:ExtensionContent";
                        break;
                    }

                case "08"
                    :
                    {
                        nsMgr.AddNamespace("tns", "urn:oasis:names:specification:ubl:schema:xsd:DebitNote-2");
                        l_xpath = "/tns:DebitNote/ext:UBLExtensions/ext:UBLExtension[1]/ext:ExtensionContent";
                        break;
                    }
            }
            nsMgr.AddNamespace("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            nsMgr.AddNamespace("udt", "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2");
            nsMgr.AddNamespace("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
            nsMgr.AddNamespace("qdt", "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2");
            nsMgr.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            nsMgr.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");

            xmlDoc.SelectSingleNode(l_xpath, nsMgr).AppendChild(xmlDoc.ImportNode(signature, true));

            return xmlDoc;
        }
        catch (Exception ex)
        {
            throw new SunatException(ex.Message);
        }
    }

    public byte[] ZipXml(XmlDocument xmlDoc, string name)
    {
        try
        {
            using var buffer = new MemoryStream();
            using (var zip = new ZipArchive(buffer, ZipArchiveMode.Create))
            {
                var entry = zip.CreateEntry(name);
                using var stream = entry.Open();
                xmlDoc.Save(stream);
            }

            return buffer.ToArray();
        }
        catch (Exception ex)
        {
            throw new SunatException(ex.Message);
        }
    }

    public async Task<byte[]> SendBill(string uri, string username, string password, string fileName, byte[] byteFile, string cdrFile)
    {
        var binding = new BasicHttpBinding(BasicHttpSecurityMode.TransportWithMessageCredential)
        {
            Security =
                {
                    Transport =
                    {
                        ClientCredentialType = HttpClientCredentialType.None,
                        ProxyCredentialType = HttpProxyCredentialType.None,
                    },
                    Message =
                    {
                        ClientCredentialType = BasicHttpMessageCredentialType.UserName,
                    }
                }
        };

        using var servicio = new billServiceClient(binding, new EndpointAddress(uri));

        try
        {
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.CheckCertificateRevocationList = true;

            servicio.ClientCredentials.UserName.UserName = username;
            servicio.ClientCredentials.UserName.Password = password;

            var customBinding = new CustomBinding(binding);

            var elements = customBinding.Elements;
            int i = -1;

            for (i = 0; i < elements.Count; i++)
            {
                if (typeof(MessageEncodingBindingElement).IsAssignableFrom(elements[i].GetType()))
                    break;
            }
            var mebe = (MessageEncodingBindingElement)elements[i];
            elements[i] = new CustomMessageEncodingBindingElement(mebe);

            servicio.Endpoint.Binding = new CustomBinding(elements);

            await servicio.OpenAsync();

            var sendBillResponse = await servicio.sendBillAsync(fileName, byteFile, "0");

            return sendBillResponse.applicationResponse;
        }
        catch (FaultException fex)
        {
            throw new SunatException(fex.Message);
        }
        finally
        {
            await servicio.CloseAsync();
        }
    }

    public List<string> ReadResponse(byte[] cdrByte)
    {
        var responses = new List<string>();
        try
        {
            using var data = new MemoryStream(cdrByte); // The original data
            using var archive = new ZipArchive(data);
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                if (entry.FullName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                {
                    XmlDocument xd = new XmlDocument();
                    xd.Load(entry.Open());
                    XmlNodeList xnl = xd.GetElementsByTagName("cbc:Description");
                    foreach (XmlElement item in xnl)
                    {
                        responses.Add(item.InnerText);
                    }
                }
            }
            return responses;
        }
        catch (Exception ex)
        {
            throw new SunatException(ex.Message);
        }
    }
}