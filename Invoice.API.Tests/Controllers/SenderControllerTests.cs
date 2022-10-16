using AutoFixture;
using Invoice.API.Controllers;
using Invoice.Service.Contracts;
using Invoice.Shared.Request;
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
}