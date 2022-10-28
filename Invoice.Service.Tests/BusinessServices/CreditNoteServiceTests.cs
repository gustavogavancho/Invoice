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
using UBLSunatPE;

namespace Invoice.Service.Tests.BusinessServices;

public class CreditNoteServiceTests
{
    private readonly Fixture _fixture;
    private readonly Mock<IRepositoryManager> _repository;
    private readonly Mock<ILoggerManager> _logger;
    private readonly Mapper _mapper;
    private readonly Mock<IDocumentGeneratorService> _documentGeneratorService;
    private readonly Mock<ISunatService> _sunatService;

    public CreditNoteServiceTests()
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
    public async Task CreditNoteService_CreateCreditNoteAsyncTest()
    {
        //Arrange
        var request = _fixture.Create<CreditNoteRequest>();
        var issuer = _fixture.Create<Issuer>();
        var invoice = _fixture.Create<Entities.Models.Invoice>();
        invoice.Canceled = false;

        _repository.Setup(x => x.Issuer.GetIssuerAsync(It.IsAny<Guid>(), false)).ReturnsAsync(issuer);
        _repository.Setup(x => x.Invoice.GetInvoiceBySerieAsync(It.IsAny<string>(), It.IsAny<uint>(), It.IsAny<uint>(), true)).ReturnsAsync(invoice);
        _repository.Setup(x => x.Invoice.CreateInvoice(It.IsAny<Entities.Models.Invoice>())).Verifiable();
        _sunatService.Setup(x => x.SerializeXmlDocument(typeof(DebitNoteType), It.IsAny<DebitNoteType>())).Returns(It.IsAny<string>());
        _sunatService.Setup(x => x.SignXml(It.IsAny<String>(), It.IsAny<Issuer>(), It.IsAny<string>())).Returns(new XmlDocument());
        _sunatService.Setup(x => x.ZipXml(It.IsAny<XmlDocument>(), It.IsAny<string>())).Returns(It.IsAny<byte[]>());
        _sunatService.Setup(x => x.SendBill(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<byte[]>())).ReturnsAsync(It.IsAny<byte[]>());
        _sunatService.Setup(x => x.ReadResponse(It.IsAny<byte[]>())).Returns(new List<string> { "La Nota de Debito numero FD01-00000001, ha sido aceptada" });

        //Act
        var debitNoteService = new CreditNoteService(_repository.Object, _logger.Object, _mapper, _documentGeneratorService.Object, _sunatService.Object);
        var sut = await debitNoteService.CreateCreditNoteAsync(It.IsAny<Guid>(), request, false);

        //Assert
        Assert.NotNull(sut);
        Assert.IsType<CreditNoteResponse>(sut);
    }

    [Fact]
    public async Task CreditNoteService_GetCreditNoteAsyncTest()
    {
        //Arrange
        var issuer = _fixture.Create<Entities.Models.Invoice>();
        var id = Guid.Parse("CCE03168-F901-4B23-AE9C-FC031D9DC888");
        _repository.Setup(x => x.Invoice.GetInvoiceAsync(id, false)).ReturnsAsync(issuer);

        //Act
        var creditNoteService = new CreditNoteService(_repository.Object, _logger.Object, _mapper, _documentGeneratorService.Object, _sunatService.Object);
        CreditNoteResponse sut = await creditNoteService.GetCreditNoteAsync(id, false);

        //Assert
        Assert.NotNull(sut);
    }
}