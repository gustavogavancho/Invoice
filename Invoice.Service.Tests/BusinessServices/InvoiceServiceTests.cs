using AutoFixture;
using AutoMapper;
using Invoice.Contracts.Logger;
using Invoice.Contracts.Repositories;
using Invoice.Entities.Models;
using Invoice.Service.BusinessServices;
using Invoice.Service.Contracts.HelperServices;
using Invoice.Service.Profiles;
using Invoice.Shared.Request;
using Invoice.Shared.Response;
using Moq;
using System.Xml;

namespace Invoice.Service.Tests.BusinessServices
{
    public class InvoiceServiceTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IRepositoryManager> _repository;
        private readonly Mock<ILoggerManager> _logger;
        private readonly Mapper _mapper;
        private readonly Mock<IDocumentGeneratorService> _documentGeneratorService;
        private readonly Mock<ISunatService> _sunatService;

        public InvoiceServiceTests()
        {
            _fixture = new Fixture();
            _repository = new Mock<IRepositoryManager>();
            _logger = new Mock<ILoggerManager>();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = new Mapper(mapperConfiguration);
            _documentGeneratorService = new Mock<IDocumentGeneratorService>();
            _sunatService = new Mock<ISunatService>();
        }

        [Fact]
        public async Task InvoiceService_CreateInvoiceAsyncTest()
        {
            //ArrangeS
            var request = _fixture.Create<InvoiceRequest>();
            var issuer = _fixture.Create<Issuer>();

            _repository.Setup(x => x.Issuer.GetIssuerAsync(It.IsAny<Guid>(), false)).ReturnsAsync(issuer);
            _repository.Setup(x => x.Invoice.CreateInvoice(It.IsAny<Entities.Models.Invoice>())).Verifiable();
            _sunatService.Setup(x => x.SignXml(It.IsAny<String>(), It.IsAny<Issuer>(), It.IsAny<string>())).Returns(new XmlDocument());
            _sunatService.Setup(x => x.ReadResponse(It.IsAny<byte[]>())).Returns(new List<string> { "La Factura numero FA01-00000001, ha sido aceptada" });

            //Act
            var invoiceService = new InvoiceService(_repository.Object, _logger.Object, _mapper, _documentGeneratorService.Object, _sunatService.Object);
            var sut = await invoiceService.CreateInvoiceAsync(It.IsAny<Guid>(), request, false);

            //Assert
            _documentGeneratorService.Verify(x => x.GenerateInvoiceType(It.IsAny<InvoiceRequest>(), It.IsAny<Issuer>()), Times.Once);
            _sunatService.Verify(x => x.SignXml(It.IsAny<string>(), It.IsAny<Issuer>(), It.IsAny<string>()), Times.Once);
            _sunatService.Verify(x => x.ZipXml(It.IsAny<XmlDocument>(), It.IsAny<string>()));
            _sunatService.Verify(x => x.SendBill(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<string>()));
            _sunatService.Verify(x => x.ReadResponse(It.IsAny<byte[]>()));
            Assert.NotNull(sut);
            Assert.IsType<InvoiceResponse>(sut);
        }

        [Fact]
        public async Task InvoiceService_GetInvoiceAsyncTest()
        {
            //Arrange
            var issuer = _fixture.Create<Entities.Models.Invoice>();
            var id = Guid.Parse("CCE03168-F901-4B23-AE9C-FC031D9DC888");
            _repository.Setup(x => x.Invoice.GetInvoiceAsync(id, false)).ReturnsAsync(issuer);

            //Act
            var invoiceService = new InvoiceService(_repository.Object, _logger.Object, _mapper, _documentGeneratorService.Object, _sunatService.Object);
            var sut = await invoiceService.GetInvoiceAsync(id, false);

            //Assert
            Assert.NotNull(sut);
        }
    }
}
