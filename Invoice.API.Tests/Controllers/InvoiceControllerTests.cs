using AutoFixture;
using Invoice.API.Controllers;
using Invoice.Service.Contracts.ServiceManagers;
using Invoice.Shared.Request;
using Invoice.Shared.Response;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Invoice.API.Tests.Controllers;

public class InvoiceControllerTests
{
    private readonly Fixture _fixture;
    private readonly Mock<IServiceManager> _service;

    public InvoiceControllerTests()
	{
        _fixture = new Fixture();
        _service = new Mock<IServiceManager>();
    }

    [Fact]
    public async Task InvoiceController_CreateInvoiceTest()
    {
        //Arrange
        var invoiceRequest = _fixture.Create<InvoiceRequest>();
        var invoiceResponse = _fixture.Create<InvoiceResponse>();
        _service.Setup(x => x.InvoiceService.CreateInvoiceAsync(It.IsAny<Guid>(), It.IsAny<InvoiceRequest>(), false)).ReturnsAsync(invoiceResponse);

        //Act
        var invoiceController = new InvoiceController(_service.Object);
        var sut = await invoiceController.CreateInvoice(It.IsAny<Guid>(), invoiceRequest);

        //Assert
        var statusCodeResult = Assert.IsType<CreatedAtRouteResult>(sut);
        Assert.Equal(201, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task InvoiceController_GetInvoicesTest()
    {
        //Arrange
        var invoices = _fixture.Create<List<InvoiceResponse>>();
        _service.Setup(x => x.InvoiceService.GetInvoicesAsync(false)).ReturnsAsync(invoices);

        //Act
        var issuerController = new InvoiceController(_service.Object);
        var sut = await issuerController.GetInvoices();

        //Assert
        var actionResult = Assert.IsType<ActionResult<List<InvoiceResponse>>>(sut);
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var response = Assert.IsType<List<InvoiceResponse>>(okObjectResult.Value);
        Assert.True(response.Count() > 1, "Expected sut to be greater than 1");
    }
}