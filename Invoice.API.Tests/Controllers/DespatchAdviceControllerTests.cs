using AutoFixture;
using Invoice.API.Controllers;
using Invoice.Service.Contracts.ServiceManagers;
using Invoice.Shared.Params;
using Invoice.Shared.Request;
using Invoice.Shared.Response;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Invoice.API.Tests.Controllers;

public class DespatchAdviceControllerTests
{

    private readonly Fixture _fixture;
    private readonly Mock<IServiceManager> _service;

    public DespatchAdviceControllerTests()
    {
        _fixture = new Fixture();
        _service = new Mock<IServiceManager>();
    }

    [Fact]
    public async Task DespatchController_CreateDespatchTest()
    {
        //Arrange
        var despatchRequest = _fixture.Create<DespatchRequest>();
        var despatchResponse = _fixture.Create<DespatchResponse>();
        _service.Setup(x => x.DespatchAdviceService.CreateDespatchAdviceAsync(It.IsAny<Guid>(), It.IsAny<DespatchRequest>(), false)).ReturnsAsync(despatchResponse);

        //Act
        var despatchController = new DespatchAdviceController(_service.Object);
        var sut = await despatchController.CreateDespatchAdvice(It.IsAny<Guid>(), despatchRequest);

        //Assert
        var statusCodeResult = Assert.IsType<OkObjectResult>(sut);
        Assert.Equal(200, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task DespatchController_GetDespatchesTest()
    {
        //Arrange
        var despatches = _fixture.Create<List<DespatchResponse>>();
        _service.Setup(x => x.DespatchAdviceService.GetDespatchesAsync(false)).ReturnsAsync(despatches);

        //Act
        var despatchesController = new DespatchAdviceController(_service.Object);
        var sut = await despatchesController.GetDespatches();

        //Assert
        var actionResult = Assert.IsType<ActionResult<List<DespatchResponse>>>(sut);
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var response = Assert.IsType<List<DespatchResponse>>(okObjectResult.Value);
        Assert.True(response.Count() > 1, "Expected sut to be greater than 1");
    }

    [Fact]
    public async Task DespatchController_GetDespatchBySerieTest()
    {
        //Arrange
        var despatch = _fixture.Create<DespatchResponse>();
        _service.Setup(x => x.DespatchAdviceService.GetDespatchAdviceBySerieAsync(It.IsAny<string>(), It.IsAny<uint>(), It.IsAny<uint>(), false)).ReturnsAsync(despatch);

        //Act
        var despatchController = new DespatchAdviceController(_service.Object);
        var sut = await despatchController.GetDespatchBySerie(new InvoiceParams());

        //Assert
        var actionResult = Assert.IsType<ActionResult<DespatchResponse>>(sut);
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var response = Assert.IsType<DespatchResponse>(okObjectResult.Value);
        Assert.Equal(response.SunatResponse, despatch.SunatResponse);
    }
}