using AutoFixture;
using Invoice.API.Controllers;
using Invoice.Service.Contracts.ServiceManagers;
using Invoice.Shared.Request;
using Invoice.Shared.Response;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Invoice.API.Tests.Controllers;

public class SummaryDocumentsControllerTests
{
    private readonly Fixture _fixture;
    private readonly Mock<IServiceManager> _service;

    public SummaryDocumentsControllerTests()
    {
        _fixture = new Fixture();
        _service = new Mock<IServiceManager>();
    }

    [Fact]
    public async Task SummaryDocumentsController_CreateSummaryDocumentsTest()
    {
        //Arrange
        var summaryDocumentsRequest = _fixture.Create<SummaryDocumentsRequest>();
        var summaryDocumentsResponse = _fixture.Create<SummaryDocumentsResponse>();
        _service.Setup(x => x.SummaryDocumentsService.CreateSummaryDocumentsAsync(It.IsAny<Guid>(), It.IsAny<SummaryDocumentsRequest>(), false)).ReturnsAsync(summaryDocumentsResponse);

        //Act
        var summaryDocumentsController = new SummaryDocumentsController(_service.Object);
        var sut = await summaryDocumentsController.CreateSummaryDocuments(It.IsAny<Guid>(), summaryDocumentsRequest);

        //Assert
        var statusCodeResult = Assert.IsType<OkObjectResult>(sut);
        Assert.Equal(200, statusCodeResult.StatusCode);
    }
}