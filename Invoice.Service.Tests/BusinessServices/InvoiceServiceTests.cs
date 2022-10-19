using AutoFixture;
using AutoMapper;
using Invoice.Contracts.Logger;
using Invoice.Contracts.Repositories;
using Invoice.Entities.Models;
using Invoice.Service.BusinessServices;
using Invoice.Service.Contracts.HelperServices;
using Invoice.Service.Contracts.ServiceManagers;
using Invoice.Shared.Request;
using Moq;

namespace Invoice.Service.Tests.BusinessServices
{
    public class InvoiceServiceTests
    {
        [Fact]
        public async Task InvoiceService_SendInvoiceTypeTest()
        {
            //Arrange
            var fixture = new Fixture();
            var request = fixture.Create<InvoiceRequest>();
            var issuer = fixture.Create<Issuer>();

            var repositoryManager = new Mock<IRepositoryManager>();
            repositoryManager.Setup(x => x.Issuer.GetIssuerAsync(It.IsAny<Guid>(), false)).ReturnsAsync(issuer);
            var loggerManager = new Mock<ILoggerManager>();
            var mapper = new Mock<IMapper>();
            var documentGeneratorService = new Mock<IDocumentGeneratorService>();
            var serializeXmlService = new Mock<ISerializeXmlService>();
            var signerService = new Mock<ISignerService>();
            var zipperService = new Mock<IZipperService>();

            var fileName = $"{issuer.IssuerId}-{request.InvoiceDetail.DocumentType}-{request.InvoiceDetail.Serie}{request.InvoiceDetail.SerialNumber.ToString("00")}-{request.InvoiceDetail.CorrelativeNumber.ToString("00000000")}.xml";
            var path = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + $"\\XML";

            var sut = new InvoiceService(repositoryManager.Object, loggerManager.Object, mapper.Object, documentGeneratorService.Object, serializeXmlService.Object, signerService.Object, zipperService.Object);

            //Act
            await sut.SendInvoiceType(It.IsAny<Guid>(), request, false);

            //Assert
            serializeXmlService.Verify(x => x.SerializeXmlDocument(fileName, path, It.IsAny<Type>(), It.IsAny<object>()), Times.Once);
            signerService.Verify(x => x.SignXml(It.IsAny<Guid>(), Path.Combine(path, fileName), It.IsAny<Issuer>()));
            zipperService.Verify(x => x.ZipXml(It.IsAny<string>()));
        }
    }
}
