using AutoFixture;
using Invoice.Service.BusinessServices;
using Invoice.Service.Contracts.HelperServices;
using Invoice.Shared.Request;
using Moq;

namespace Invoice.Service
{
    public class InvoiceServiceTests
    {
        [Fact]
        public async Task InvoiceService_SendInvoiceTypeTest()
        {
            //Arrange
            var fixture = new Fixture();
            var request = fixture.Create<InvoiceRequest>();

            #region Fix amount

            request.TaxTotalAmount = 3.6m;
            request.TotalAmount = 23.6m;

            foreach (var item in request.TaxSubTotal)
            {
                item.TaxableAmount = 20;
                item.TaxAmount = 3.6m;
            }

            foreach (var item in request.ProductsDetail)
            {
                item.Quantity = 1;
                item.UnitPrice = 20;
                item.TaxAmount = 3.6m;
                item.TaxPercentage = 18;
            }

            #endregion

            var mockSerializeXmlService = new Mock<ISerializeXmlService>();
            var mockSignerService = new Mock<ISignerService>();
            var mockZipperService = new Mock<IZipperService>();

            var fileName = $"{request.Issuer.IssuerId}-{request.InvoiceData.DocumentType}-{request.InvoiceData.Serie}{request.InvoiceData.SerialNumber.ToString("00")}-{request.InvoiceData.CorrelativeNumber.ToString("00000000")}.xml";
            var path = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + $"\\XML";

            mockSerializeXmlService.Setup(x => x.SerializeXmlDocument(fileName, path, It.IsAny<Type>(), It.IsAny<object>())).Verifiable();

            var sut = new InvoiceService(mockSerializeXmlService.Object, mockSignerService.Object, mockZipperService.Object);

            //Act
            await sut.SendInvoiceType(It.IsAny<Guid>(), request);

            //Assert
            mockSerializeXmlService.Verify(x => x.SerializeXmlDocument(fileName, path, It.IsAny<Type>(), It.IsAny<object>()), Times.Once);
        }
    }
}
