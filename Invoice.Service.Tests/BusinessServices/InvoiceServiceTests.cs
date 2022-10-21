using AutoFixture;
using AutoMapper;
using Invoice.Contracts.Logger;
using Invoice.Contracts.Repositories;
using Invoice.Entities.Models;
using Invoice.Service.BusinessServices;
using Invoice.Service.Contracts.HelperServices;
using Invoice.Shared.Request;
using Moq;
using System.Xml;

namespace Invoice.Service.Tests.BusinessServices
{
    public class InvoiceServiceTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IRepositoryManager> _repositoryManager;
        private readonly Mock<ILoggerManager> _loggerManager;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IDocumentGeneratorService> _documentGeneratorService;
        private readonly Mock<ISunatService> _sunatService;

        public InvoiceServiceTests()
        {
            _fixture = new Fixture();
            _repositoryManager = new Mock<IRepositoryManager>();
            _loggerManager = new Mock<ILoggerManager>();
            _mapper = new Mock<IMapper>();
            _documentGeneratorService = new Mock<IDocumentGeneratorService>();
            _sunatService = new Mock<ISunatService>();
        }

        [Fact]
        public async Task InvoiceService_SendInvoiceTypeTest()
        {
            //ArrangeS
            var request = _fixture.Create<InvoiceRequest>();
            var issuer = _fixture.Create<Issuer>();

            _repositoryManager.Setup(x => x.Issuer.GetIssuerAsync(It.IsAny<Guid>(), false)).ReturnsAsync(issuer);

            var sut = new InvoiceService(_repositoryManager.Object, _loggerManager.Object, _mapper.Object, _documentGeneratorService.Object, _sunatService.Object);

            //Act
            await sut.SendInvoiceType(It.IsAny<Guid>(), request, false);

            //Assert
            _documentGeneratorService.Verify(x => x.GenerateInvoiceType(It.IsAny<InvoiceRequest>(), It.IsAny<Issuer>()), Times.Once);
            _sunatService.Verify(x => x.SignXml(It.IsAny<string>(), It.IsAny<Issuer>(), It.IsAny<string>()), Times.Once);
            _sunatService.Verify(x => x.ZipXml(It.IsAny<XmlDocument>(), It.IsAny<string>()));
            _sunatService.Verify(x => x.SendBill(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<string>()));
            _sunatService.Verify(x => x.ReadResponse(It.IsAny<byte[]>()));
        }
    }
}
