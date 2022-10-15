using System.Reflection;
using UBLSunatPE;

namespace Invoice.Service
{
    public class InvoiceSerializerServiceTests
    {
        [Fact]
        public void SerializeInvoiceTypeTest()
        {
            //Arrange
            var service = new InvoiceSerializerService();

            MethodInfo methodInfo = service.GetType()
                .GetMethod("SerializeXmlDocument",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

            var fileName = "InvoiceTypeTest.xml";
            var path = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + $"\\XMLTests";

            object[] parameters =
            {
                fileName,
                path,
                typeof(InvoiceType),
                new InvoiceType
                {
                    UBLVersionID = new UBLVersionIDType { Value = "2.1" },
                    CustomizationID = new CustomizationIDType { Value = "2.0"}
                }
            };

            //Act
            var sut = Record.Exception(() => methodInfo.Invoke(service, parameters));

            //Assert
            Assert.Null(sut);
            Assert.True(File.Exists($"{path}\\{fileName}"));
        }
    }
}
