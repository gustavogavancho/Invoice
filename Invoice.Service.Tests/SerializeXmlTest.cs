using UBLSunatPE;

namespace Invoice.Service
{
    public class SerializeXmlTest
    {
        [Fact]
        public void SerializeXmlDocumentTest()
        {
            //Arrange
            var service = new SerializeXmlService();
            var fileName = "InvoiceTypeTest.xml";
            var path = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + "\\XML";
            var documentType = typeof(InvoiceType);
            var document = new InvoiceType
            {
                UBLVersionID = new UBLVersionIDType { Value = "2.1" },
                CustomizationID = new CustomizationIDType {  Value = "2.0" }
            };

            //Act
            service.SerializeXmlDocument(fileName, path, documentType, document);

            //Assert
            Assert.True(File.Exists($"{path}\\{fileName}"));
        }
    }
}
