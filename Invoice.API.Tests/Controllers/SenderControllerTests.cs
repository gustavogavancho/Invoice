using AutoFixture;
using Invoice.API.Controllers;
using Invoice.Service;
using Invoice.Service.Contracts;
using Invoice.Shared.Request;
using Invoice.Shared.Response;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Invoice.API.Tests.Controllers;

public class SenderControllerTests
{
    [Fact]
    public async Task CreateSenderTest()
    {
        //Arrange
        var fixture = new Fixture();
        var senderRequest = fixture.Create<SenderDataRequest>();
        var senderService = new Mock<ISenderService>();
        senderService.Setup(x => x.CreateSender(senderRequest)).Verifiable();

        //Act
        var senderController = new SenderController(senderService.Object);
        var sut = await senderController.CreateSender(senderRequest);

        //Assert
        senderService.Verify(x => x.CreateSender(senderRequest), Times.Once);
        var statusCodeResult = Assert.IsType<StatusCodeResult>(sut);
        Assert.Equal(201, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task GetSendersTest()
    {
        //Arrange
        var fixture = new Fixture();
        var senders = fixture.Create<List<SenderResponse>>();
        var senderService = new Mock<ISenderService>();
        senderService.Setup(x => x.GetSenders()).ReturnsAsync(senders);

        //Act
        var senderController = new SenderController(senderService.Object);
        var sut = await senderController.GetSenders();

        //Assert
        var actionResult = Assert.IsType<ActionResult<List<SenderResponse>>>(sut);

        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);

        var response = Assert.IsType<List<SenderResponse>>(okObjectResult.Value);

        Assert.Equal(senders.FirstOrDefault().SenderName, response.FirstOrDefault().SenderName);
    }

    [Fact]
    public async Task GetSenderTest()
    {
        //Arrange
        var fixture = new Fixture();
        var sender = fixture.Create<SenderResponse>();
        var senderService = new Mock<ISenderService>();
        senderService.Setup(x => x.GetSender(sender.Id)).ReturnsAsync(sender);

        //Act
        var senderController = new SenderController(senderService.Object);
        var sut = await senderController.GetSender(sender.Id);

        //Assert
        var actionResult = Assert.IsType<ActionResult<SenderResponse>>(sut);

        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);

        var response = Assert.IsType<SenderResponse>(okObjectResult.Value);

        Assert.Equal(response.SenderName, sender.SenderName);
    }

    [Fact]
    public async Task UpdateSenderTest()
    {
        //Arrange
        var fixture = new Fixture();
        var id = Guid.Parse("CCE03168-F901-4B23-AE9C-FC031D9DC888");
        var senderDataRequest = fixture.Create<SenderDataRequest>();
        var senderService = new Mock<ISenderService>();
        senderService.Setup(x => x.UpdateSender(id, senderDataRequest)).Verifiable();

        //Act
        var senderController = new SenderController(senderService.Object);
        var sut = await senderController.UpdateSender(id, senderDataRequest);

        //Assert
        senderService.Verify(x => x.UpdateSender(id, senderDataRequest), Times.Once);
        var statusCodeResult = Assert.IsType<NoContentResult>(sut);
        Assert.Equal(204, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task DeleteSenderTest()
    {
        //Arrange
        var fixture = new Fixture();
        var id = Guid.Parse("CCE03168-F901-4B23-AE9C-FC031D9DC888");

        var senderService = new Mock<ISenderService>();
        senderService.Setup(x => x.DeleteSender(id)).Verifiable();

        //Act
        var senderController = new SenderController(senderService.Object);
        IActionResult sut = await senderController.DeleteSender(id);

        //Assert
        senderService.Verify(x => x.DeleteSender(id), Times.Once);
        var statusCodeResult = Assert.IsType<OkResult>(sut);
        Assert.Equal(200, statusCodeResult.StatusCode);
    }
}