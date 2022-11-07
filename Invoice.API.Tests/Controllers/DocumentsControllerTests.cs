using AutoFixture;
using Invoice.API.Controllers;
using Invoice.Service.Contracts.ServiceManagers;
using Invoice.Shared.Request;
using Invoice.Shared.Response;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Invoice.API.Tests.Controllers;

public class DocumentsControllerTests
{
    private readonly Fixture _fixture;
    private readonly Mock<IServiceManager> _service;

    public DocumentsControllerTests()
    {
        _fixture = new Fixture();
        _service = new Mock<IServiceManager>();
    }

    [Fact]
    public async Task DocumentsController_CreateSummaryDocumentsTest()
    {
        //Arrange
        var summaryDocumentsRequest = _fixture.Create<SummaryDocumentsRequest>();
        var summaryDocumentsResponse = _fixture.Create<DocumentsResponse>();
        _service.Setup(x => x.SummaryDocumentsService.CreateSummaryDocumentsAsync(It.IsAny<Guid>(), It.IsAny<SummaryDocumentsRequest>(), false)).ReturnsAsync(summaryDocumentsResponse);

        //Act
        var summaryDocumentsController = new DocumentsController(_service.Object);
        var sut = await summaryDocumentsController.CreateSummaryDocuments(It.IsAny<Guid>(), summaryDocumentsRequest);

        //Assert
        var statusCodeResult = Assert.IsType<OkObjectResult>(sut);
        Assert.Equal(200, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task DocumentsController_CreateVoidedDocumentsTest()
    {
        //Arrange
        var voidedDocumentsRequest = _fixture.Create<VoidedDocumentsRequest>();
        var voidedDocumentsResponse = _fixture.Create<DocumentsResponse>();
        _service.Setup(x => x.VoidedDocumentsService.CreateVoidedDocumentsAsync(It.IsAny<Guid>(), It.IsAny<VoidedDocumentsRequest>(), false)).ReturnsAsync(voidedDocumentsResponse);

        //Act
        var voidedDocumentsController = new DocumentsController(_service.Object);
        var sut = await voidedDocumentsController.CreateVoidedDocuments(It.IsAny<Guid>(), voidedDocumentsRequest);

        //Assert
        var statusCodeResult = Assert.IsType<OkObjectResult>(sut);
        Assert.Equal(200, statusCodeResult.StatusCode);
    }
}