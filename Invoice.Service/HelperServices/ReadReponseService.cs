using Invoice.Entities.Exceptions;
using Invoice.Service.Contracts.HelperServices;
using System.IO.Compression;
using System.Xml;

namespace Invoice.Service.HelperServices;

public class ReadResponseService : IReadResponseService
{
    public string[] ReadResponse(string file)
    {
        string r = "";
        string fileEntry = "";
        string[] datos = new string[3];

        try
        {
            using ZipArchive zip = ZipFile.Open(file, ZipArchiveMode.Read);
            if (zip is not null)
            {
                fileEntry = zip.Entries[1].ToString();
                ZipArchiveEntry zentry = zip.GetEntry(fileEntry);
                XmlDocument xd = new XmlDocument();
                xd.Load(zentry.Open());
                XmlNodeList xnl = xd.GetElementsByTagName("cbc:Description");
                foreach (XmlElement item in xnl)
                {
                    r = item.InnerText;
                }
            }

            datos[0] = r;
            datos[1] = fileEntry;
            datos[2] = Path.GetFileName(file);

            return datos;
        }
        catch (Exception ex)
        {
            throw new ReadResponseException(ex.Message);
        }
    }
}