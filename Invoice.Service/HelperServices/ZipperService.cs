using Aspose.Zip;
using Aspose.Zip.Saving;
using Invoice.Entities.Exceptions;
using Invoice.Service.Contracts.HelperServices;
using System.Text;

namespace Invoice.Service.HelperServices;

public class ZipperService : IZipperService
{
    public void ZipXml(string file)
    {
        try
        {
            string envioArchivoZip = Path.GetFileName(file).Replace(".xml", ".zip");

            var path = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + $"\\XMLZipped";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (FileStream zipFile = File.Open($"{path}\\{envioArchivoZip}", FileMode.Create))
            {
                using (FileStream source = File.Open(file, FileMode.Open, FileAccess.Read))
                {
                    using (var archive = new Archive(new ArchiveEntrySettings()))
                    {
                        archive.CreateEntry(Path.GetFileName(file), source);
                        archive.Save(zipFile);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new ZipperException(ex.Message);
        }
    }
}