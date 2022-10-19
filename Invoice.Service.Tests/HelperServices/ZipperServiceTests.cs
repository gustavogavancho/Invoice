using Invoice.Service.HelperServices;

namespace Invoice.Service.Tests.HelperServices
{
    public class ZipperServiceTests
    {
        [Fact]
        public void ZipperService_ZipXmlTest()
        {
            //Arrange
            var fileName = "20606022779-01-FA01-00000001.xml";
            var fileNameZipped = "20606022779-01-FA01-00000001.zip";
            var path = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + "\\XML";
            var pathZipped = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + "\\XMLZipped";

            //Act
            var zipperService = new ZipperService();
            zipperService.ZipXml(Path.Combine(path, fileName));

            //Assert
            Assert.True(File.Exists(Path.Combine(pathZipped, fileNameZipped)));
        }
    }
}
