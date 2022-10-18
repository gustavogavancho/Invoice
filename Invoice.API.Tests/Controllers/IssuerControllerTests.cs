using AutoFixture;
using Invoice.API.Controllers;
using Invoice.Service.Contracts.BusinessServices;
using Invoice.Shared.Request;
using Invoice.Shared.Response;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Invoice.API.Tests.Controllers;

public class IssuerControllerTests
{
    private readonly Fixture _fixture;
    private readonly Mock<IIssuerService> _issuerService;

    public IssuerControllerTests()
    {
        _fixture = new Fixture();
        _issuerService = new Mock<IIssuerService>();
    }

    [Fact]
    public async Task IssuerController_CreateIssuerTest()
    {
        //Arrange
        var issuerRequest = _fixture.Create<IssuerRequest>();

        //Act
        var issuerController = new IssuerController(_issuerService.Object);
        var sut = await issuerController.CreateIssuer(issuerRequest);

        //Assert
        _issuerService.Verify(x => x.CreateIssuer(issuerRequest), Times.Once);
        var statusCodeResult = Assert.IsType<StatusCodeResult>(sut);
        Assert.Equal(201, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task IssuerController_GetIssuersTest()
    {
        //Arrange
        var issuers = _fixture.Create<List<IssuerResponse>>();
        _issuerService.Setup(x => x.GetIssuers()).ReturnsAsync(issuers);

        //Act
        var issuerController = new IssuerController(_issuerService.Object);
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
        _issuerService.Setup(x => x.GetIssuer(issuer.Id)).ReturnsAsync(issuer);

        //Act
        var issuerController = new IssuerController(_issuerService.Object);
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

        //Act
        var issuerController = new IssuerController(_issuerService.Object);
        var sut = await issuerController.UpdateIssuer(id, issuerRequest);

        //Assert
        _issuerService.Verify(x => x.UpdateIssuer(id, issuerRequest), Times.Once);
        var statusCodeResult = Assert.IsType<NoContentResult>(sut);
        Assert.Equal(204, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task IssuerController_DeleteIssuerTest()
    {
        //Arrange
        var id = Guid.Parse("CCE03168-F901-4B23-AE9C-FC031D9DC888");

        //Act
        var issuerController = new IssuerController(_issuerService.Object);
        IActionResult sut = await issuerController.DeleteIssuer(id);

        //Assert
        _issuerService.Verify(x => x.DeleteIssuer(id), Times.Once);
        var statusCodeResult = Assert.IsType<OkResult>(sut);
        Assert.Equal(200, statusCodeResult.StatusCode);
    }
}