using AutoFixture;
using Invoice.API.Controllers;
using Invoice.Service.Contracts.ServiceManagers;
using Invoice.Shared.Response;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Invoice.API.Tests.Controllers;

public class TicketControllerTests
{
    private readonly Fixture _fixture;
    private readonly Mock<IServiceManager> _service;

    public TicketControllerTests()
    {
        _fixture = new Fixture();
        _service = new Mock<IServiceManager>();
    }

    [Fact]
    public async Task TicketController_GetTicketTest()
    {
        //Arrange
        var ticket = _fixture.Create<TicketResponse>();
        _service.Setup(x => x.TicketService.GetTicketAsync(ticket.TicketNumber, false)).ReturnsAsync(ticket);

        //Act
        var ticketController = new TicketController(_service.Object);
        ActionResult<TicketResponse> sut = await ticketController.GetTicket(ticket.TicketNumber);

        //Assert
        var actionResult = Assert.IsType<ActionResult<TicketResponse>>(sut);
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var response = Assert.IsType<TicketResponse>(okObjectResult.Value);
        Assert.Equal(response.TicketNumber, ticket.TicketNumber);
    }
}