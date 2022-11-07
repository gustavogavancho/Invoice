using AutoFixture;
using Invoice.API.Controllers;
using Invoice.Service.Contracts.ServiceManagers;
using Invoice.Shared.Request;
using Invoice.Shared.Response;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Invoice.API.Tests.Controllers;

public class CreditNoteControllerTests
{
    private readonly Fixture _fixture;
    private readonly Mock<IServiceManager> _service;

    public CreditNoteControllerTests()
    {
        _fixture = new Fixture();
        _service = new Mock<IServiceManager>();
    }

    [Fact]
    public async Task CreditNoteController_CreateCreditNoteTest()
    {
        //Arrange
        var creditNoteRequest = _fixture.Create<NoteRequest>();
        var creditNoteResponse = _fixture.Create<InvoiceResponse>();
        _service.Setup(x => x.CreditNoteService.CreateCreditNoteAsync(It.IsAny<Guid>(), It.IsAny<NoteRequest>(), false)).ReturnsAsync(creditNoteResponse);

        //Act
        var creditNoteController = new NoteController(_service.Object);
        var sut = await creditNoteController.CreateCreditNote(It.IsAny<Guid>(), creditNoteRequest);

        //Assert
        var statusCodeResult = Assert.IsType<CreatedAtRouteResult>(sut);
        Assert.Equal(201, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task CreditNoteController_GetCreditNoteTest()
    {
        //Arrange
        var debitNote = _fixture.Create<InvoiceResponse>();
        _service.Setup(x => x.CreditNoteService.GetCreditNoteAsync(debitNote.Id, false)).ReturnsAsync(debitNote);

        //Act
        var creditNoteController = new NoteController(_service.Object);
        var sut = await creditNoteController.GetCreditNote(debitNote.Id);

        //Assert
        var actionResult = Assert.IsType<ActionResult<InvoiceResponse>>(sut);
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var response = Assert.IsType<InvoiceResponse>(okObjectResult.Value);
        Assert.Equal(response.Observations, debitNote.Observations);
    }

    [Fact]
    public async Task DebitNoteController_CreateDebitNoteTest()
    {
        //Arrange
        var debitNoteRequest = _fixture.Create<NoteRequest>();
        var debitNoteResponse = _fixture.Create<InvoiceResponse>();
        _service.Setup(x => x.DebitNoteService.CreateDebitNoteAsync(It.IsAny<Guid>(), It.IsAny<NoteRequest>(), false)).ReturnsAsync(debitNoteResponse);

        //Act
        var debitNoteController = new NoteController(_service.Object);
        var sut = await debitNoteController.CreateDebitNote(It.IsAny<Guid>(), debitNoteRequest);

        //Assert
        var statusCodeResult = Assert.IsType<CreatedAtRouteResult>(sut);
        Assert.Equal(201, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task DebitNoteController_GetDebitNoteTest()
    {
        //Arrange
        var debitNote = _fixture.Create<InvoiceResponse>();
        _service.Setup(x => x.DebitNoteService.GetDebitNoteAsync(debitNote.Id, false)).ReturnsAsync(debitNote);

        //Act
        var debitNoteController = new NoteController(_service.Object);
        var sut = await debitNoteController.GetDebitNote(debitNote.Id);

        //Assert
        var actionResult = Assert.IsType<ActionResult<InvoiceResponse>>(sut);
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var response = Assert.IsType<InvoiceResponse>(okObjectResult.Value);
        Assert.Equal(response.Observations, debitNote.Observations);
    }
}