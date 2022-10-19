using AutoFixture;
using Invoice.API.Controllers;
using Invoice.Service.Contracts.ServiceManagers;
using Invoice.Shared.Request;
using Invoice.Shared.Response;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Invoice.API.Tests.Controllers;

public class IssuerControllerTests
{
    private readonly Fixture _fixture;
    private readonly Mock<IServiceManager> _service;

    public IssuerControllerTests()
    {
        _fixture = new Fixture();
        _service = new Mock<IServiceManager>();
    }

    [Fact]
    public async Task IssuerController_CreateIssuerTest()
    {
        //Arrange
        var issuerRequest = _fixture.Create<IssuerRequest>();
        var issuerResponse = _fixture.Create<IssuerResponse>();
        _service.Setup(x => x.IssuerService.CreateIssuerAsync(It.IsAny<IssuerRequest>())).ReturnsAsync(issuerResponse);

        //Act
        var issuerController = new IssuerController(_service.Object);
        var sut = await issuerController.CreateIssuer(issuerRequest);

        //Assert
        var statusCodeResult = Assert.IsType<CreatedAtRouteResult>(sut);
        Assert.Equal(201, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task IssuerController_GetIssuersTest()
    {
        //Arrange
        var issuers = _fixture.Create<List<IssuerResponse>>();
        _service.Setup(x => x.IssuerService.GetIssuersAsync(false)).ReturnsAsync(issuers);

        //Act
        var issuerController = new IssuerController(_service.Object);
        var sut = await issuerController.GetIssuers();

        //Assert
        var actionResult = Assert.IsType<ActionResult<List<IssuerResponse>>>(sut);
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var response = Assert.IsType<List<IssuerResponse>>(okObjectResult.Value);
        Assert.Equal(issuers.FirstOrDefault().IssuerName, response.FirstOrDefault().IssuerName);
    }

    [Fact]
    public async Task IssuerController_GetIssuerTest()
    {
        //Arrange
        var issuer = _fixture.Create<IssuerResponse>();
        _service.Setup(x => x.IssuerService.GetIssuerAsync(issuer.Id, false)).ReturnsAsync(issuer);

        //Act
        var issuerController = new IssuerController(_service.Object);
        var sut = await issuerController.GetIssuer(issuer.Id);

        //Assert
        var actionResult = Assert.IsType<ActionResult<IssuerResponse>>(sut);
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var response = Assert.IsType<IssuerResponse>(okObjectResult.Value);
        Assert.Equal(response.IssuerName, issuer.IssuerName);
    }

    [Fact]
    public async Task IssuerController_UpdateIssuerTest()
    {
        //Arrange
        var issuerRequest = _fixture.Create<IssuerRequest>();
        var id = Guid.Parse("CCE03168-F901-4B23-AE9C-FC031D9DC888");
        _service.Setup(x => x.IssuerService.GetIssuerAsync(It.IsAny<Guid>(), false));

        //Act
        var issuerController = new IssuerController(_service.Object);
        var sut = await issuerController.UpdateIssuer(id, issuerRequest);

        //Assert
        _service.Verify(x => x.IssuerService.UpdateIssuerAsync(id, issuerRequest, true), Times.Once);
        var statusCodeResult = Assert.IsType<NoContentResult>(sut);
        Assert.Equal(204, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task IssuerController_DeleteIssuerTest()
    {
        //Arrange
        var id = Guid.Parse("CCE03168-F901-4B23-AE9C-FC031D9DC888");
        _service.Setup(x => x.IssuerService.GetIssuerAsync(It.IsAny<Guid>(), false));

        //Act
        var issuerController = new IssuerController(_service.Object);
        IActionResult sut = await issuerController.DeleteIssuer(id);

        //Assert
        _service.Verify(x => x.IssuerService.DeleteIssuerAsync(id, false), Times.Once);
        var statusCodeResult = Assert.IsType<NoContentResult>(sut);
        Assert.Equal(204, statusCodeResult.StatusCode);
    }
}