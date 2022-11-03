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

public class SummaryDocumentsServiceTests
{
    private readonly Fixture _fixture;
    private readonly Mock<IRepositoryManager> _repository;
    private readonly Mock<ILoggerManager> _logger;
    private readonly Mapper _mapper;
    private readonly Mock<IDocumentGeneratorService> _documentGeneratorService;
    private readonly Mock<ISunatService> _sunatService;

    public SummaryDocumentsServiceTests()
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
    public async Task SummaryDocumentsService_CreateSummaryDocumentsAsyncTest()
    {
        //Arrange
        var request = _fixture.Create<SummaryDocumentsRequest>();
        var issuer = _fixture.Create<Issuer>();
        var invoices = _fixture.Create<IEnumerable<Entities.Models.Invoice>>();
        var summaryDocuments = new SummaryDocumentsType
        {
            ID = new IDType { Value = $"RC-{request.IssueDate.ToString("yyyyMMdd")}-{request.SummaryDocumentsId.ToString("00000")}" }
        };

        #region Fix invoices

        request.ReferenceDate = new DateTime(2022, 10, 30);

        foreach (var invoice in invoices)
        {
            invoice.Canceled = false;
            invoice.InvoiceDetail.DocumentType = "03";
            invoice.SummaryDocumentStatus = null;
            invoice.IssueDate = new DateTime(2022, 10, 30);
        }

        #endregion

        _repository.Setup(x => x.Issuer.GetIssuerAsync(It.IsAny<Guid>(), false)).ReturnsAsync(issuer);
        _repository.Setup(x => x.Invoice.GetTicketsByIssueDateAsync(new DateTime(2022, 10, 30), null, false)).ReturnsAsync(invoices);
        _repository.Setup(x => x.Ticket.CreateTicket(It.IsAny<Ticket>())).Verifiable();
        _documentGeneratorService.Setup(x => x.GenerateSummaryDocumentsType(It.IsAny<SummaryDocumentsRequest>(), It.IsAny<Issuer>(), It.IsAny<IEnumerable<Entities.Models.Invoice>>())).Returns(summaryDocuments);
        _sunatService.Setup(x => x.SerializeXmlDocument(typeof(DebitNoteType), It.IsAny<DebitNoteType>())).Returns(It.IsAny<string>());
        _sunatService.Setup(x => x.SignXml(It.IsAny<String>(), It.IsAny<Issuer>(), It.IsAny<string>())).Returns(new XmlDocument());
        _sunatService.Setup(x => x.ZipXml(It.IsAny<XmlDocument>(), It.IsAny<string>())).Returns(It.IsAny<byte[]>());
        _sunatService.Setup(x => x.SendSummary(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<byte[]>())).ReturnsAsync("1234567");

        //Act
        var summaryDocumentsService = new SummaryDocumentsService(_repository.Object, _logger.Object, _mapper, _documentGeneratorService.Object, _sunatService.Object);
        var sut = await summaryDocumentsService.CreateSummaryDocumentsAsync(It.IsAny<Guid>(), request, false);

        //Assert
        Assert.NotNull(sut);
        Assert.IsType<SummaryDocumentsResponse>(sut);
    }
}