using AutoFixture;
using AutoMapper;
using Invoice.Contracts.Logger;
using Invoice.Contracts.Repositories;
using Invoice.Entities.ConfigurationModels;
using Invoice.Entities.Models;
using Invoice.Service.BusinessServices;
using Invoice.Service.Contracts.HelperServices;
using Invoice.Service.Profiles;
using Invoice.Shared.Request;
using Invoice.Shared.Response;
using Microsoft.Extensions.Options;
using Moq;
using System.Xml;
using UBLSunatPE;

namespace Invoice.Service.Tests.BusinessServices;

public class VoidedDocumetsServiceTests
{
    private readonly Fixture _fixture;
    private readonly Mock<IRepositoryManager> _repository;
    private readonly Mock<ILoggerManager> _logger;
    private readonly Mapper _mapper;
    private readonly Mock<IDocumentGeneratorService> _documentGeneratorService;
    private readonly Mock<ISunatService> _sunatService;
    private readonly Mock<IOptions<SunatConfiguration>> _configuration;

    public VoidedDocumetsServiceTests()
    {
        _fixture = new Fixture();
        _repository = new Mock<IRepositoryManager>();
        _logger = new Mock<ILoggerManager>();
        var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        _mapper = new Mapper(mapperConfiguration);
        _documentGeneratorService = new Mock<IDocumentGeneratorService>();
        _sunatService = new Mock<ISunatService>();
        _configuration = new Mock<IOptions<SunatConfiguration>>();
    }

    [Fact]
    public async Task VoidedDocumentsService_CreateVoidedDocumentsAsyncTest()
    {
        //Arrange
        var request = _fixture.Create<VoidedDocumentsRequest>();
        var issuer = _fixture.Create<Issuer>();
        var invoice = _fixture.Create<Entities.Models.Invoice>();
        var sunatConfiguration = _fixture.Create<SunatConfiguration>();
        invoice.Canceled = false;
        var voidedDocuments = new VoidedDocumentsType
        {
            ID = new IDType { Value = $"RC-{request.IssueDate.ToString("yyyyMMdd")}-{request.VoidedDocumentsId.ToString("00000")}" }
        };

        _repository.Setup(x => x.Issuer.GetIssuerAsync(It.IsAny<Guid>(), true)).ReturnsAsync(issuer);
        _repository.Setup(x => x.Ticket.CreateTicket(It.IsAny<Ticket>())).Verifiable();
        _configuration.Setup(x => x.Value).Returns(sunatConfiguration);
        _repository.Setup(x => x.Invoice.GetInvoiceBySerieAsync(It.IsAny<string>(), It.IsAny<uint>(), It.IsAny<uint>(), true)).ReturnsAsync(invoice);
        _documentGeneratorService.Setup(x => x.GenerateVoidedDocumentsType(It.IsAny<VoidedDocumentsRequest>(), It.IsAny<Issuer>())).Returns(voidedDocuments);
        _sunatService.Setup(x => x.SerializeXmlDocument(typeof(InvoiceType), It.IsAny<InvoiceType>())).Returns(It.IsAny<string>());
        _sunatService.Setup(x => x.SignXml(It.IsAny<String>(), It.IsAny<Issuer>(), It.IsAny<string>())).Returns(new XmlDocument());
        _sunatService.Setup(x => x.ZipXml(It.IsAny<XmlDocument>(), It.IsAny<string>())).Returns(It.IsAny<byte[]>());
        _sunatService.Setup(x => x.SendSummary(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<byte[]>())).ReturnsAsync("1234567");
        _sunatService.Setup(x => x.ReadResponse(It.IsAny<byte[]>())).Returns(new List<string> { "La Factura numero FA01-00000001, ha sido aceptada" });

        //Act
        var voidedDocumentsService = new VoidedDocumentsService(_repository.Object, _logger.Object, _mapper, _documentGeneratorService.Object, _sunatService.Object, _configuration.Object);
        var sut = await voidedDocumentsService.CreateVoidedDocumentsAsync(It.IsAny<Guid>(), request, true);

        //Assert
        Assert.NotNull(sut);
        Assert.IsType<DocumentsResponse>(sut);
    }
}