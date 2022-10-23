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
    public async Task InvoiceController_GetInvoiceTest()
    {
        //Arrange
        var invoice = _fixture.Create<InvoiceResponse>();
        _service.Setup(x => x.InvoiceService.GetInvoiceAsync(invoice.Id, false)).ReturnsAsync(invoice);

        //Act
        var issuerController = new InvoiceController(_service.Object);
        var sut = await issuerController.GetInvoice(invoice.Id);

        //Assert
        var actionResult = Assert.IsType<ActionResult<InvoiceResponse>>(sut);
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var response = Assert.IsType<InvoiceResponse>(okObjectResult.Value);
        Assert.Equal(response.Observations, invoice.Observations);
    }
}