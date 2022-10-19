using Invoice.Service.HelperServices;
using UBLSunatPE;

namespace Invoice.Service.Tests.HelperServices
{
    public class SerializeXmlTest
    {
        [Fact]
        public void SerializeXmlService_SerializeXmlDocumentTest()
        {
            //Arrange
            var service = new SerializeXmlService();
            var fileName = "20606022779-01-FA01-00000001.xml";
            var path = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + "\\XML";
            var documentType = typeof(InvoiceType);
            var document = new InvoiceType
            {
                UBLVersionID = new UBLVersionIDType { Value = "2.1" },
                CustomizationID = new CustomizationIDType { Value = "2.0" },
                UBLExtensions = new UBLExtensionType[] { new UBLExtensionType() },
            };

            //Act
            service.SerializeXmlDocument(fileName, path, documentType, document);

            //Assert
            Assert.True(File.Exists(Path.Combine(path, fileName)));
        }
    }
}
