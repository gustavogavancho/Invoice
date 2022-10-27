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
    public async Task DebitNoteController_CreateDebitNoteTest()
    {
        //Arrange
        var debitNoteRequest = _fixture.Create<DebitNoteRequest>();
        var debitNoteResponse = _fixture.Create<DebitNoteResponse>();
        _service.Setup(x => x.DebitNoteService.CreateDebitNoteAsync(It.IsAny<Guid>(), It.IsAny<DebitNoteRequest>(), false)).ReturnsAsync(debitNoteResponse);

        //Act
        var debitNoteController = new DebitNoteController(_service.Object);
        var sut = await debitNoteController.CreateDebitNote(It.IsAny<Guid>(), debitNoteRequest);

        //Assert
        var statusCodeResult = Assert.IsType<CreatedAtRouteResult>(sut);
        Assert.Equal(201, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task DebitNoteController_GetDebitNoteTest()
    {
        //Arrange
        var debitNote = _fixture.Create<DebitNoteResponse>();
        _service.Setup(x => x.DebitNoteService.GetDebitNoteAsync(debitNote.Id, false)).ReturnsAsync(debitNote);

        //Act
        var debitNoteController = new DebitNoteController(_service.Object);
        ActionResult<DebitNoteResponse> sut = await debitNoteController.GetDebitNote(debitNote.Id);

        //Assert
        var actionResult = Assert.IsType<ActionResult<DebitNoteResponse>>(sut);
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var response = Assert.IsType<DebitNoteResponse>(okObjectResult.Value);
        Assert.Equal(response.Observations, debitNote.Observations);
    }
}