using AutoFixture;
using Invoice.API.Controllers;
using Invoice.Service.Contracts.ServiceManagers;
using Invoice.Shared.Request;
using Invoice.Shared.Response;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Invoice.API.Tests.Controllers;

public class DebitNoteControllerTests
{
    private readonly Fixture _fixture;
    private readonly Mock<IServiceManager> _service;

    public DebitNoteControllerTests()
    {
        _fixture = new Fixture();
        _service = new Mock<IServiceManager>();
    }

    [Fact]
    public async Task InvoiceController_CreateDebitNoteTest()
    {
        //Arrange
        var debitNoteRequest = _fixture.Create<DebitNoteRequest>();
        var debitNoteResponse = _fixture.Create<DebitNoteResponse>();
        _service.Setup(x => x.InvoiceService.CreateDebitNoteAsync(It.IsAny<Guid>(), It.IsAny<DebitNoteRequest>(), false)).ReturnsAsync(debitNoteResponse);

        //Act
        var debitNoteController = new DebitNoteController(_service.Object);
        var sut = await debitNoteController.CreateDebitNote(It.IsAny<Guid>(), debitNoteRequest);

        //Assert
        var statusCodeResult = Assert.IsType<OkObjectResult>(sut);
        Assert.Equal(200, statusCodeResult.StatusCode);
    }
}