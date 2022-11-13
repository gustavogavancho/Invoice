using Invoice.API.ActionFilters;
using Invoice.Service.Contracts.ServiceManagers;
using Invoice.Shared.Params;
using Invoice.Shared.Request;
using Invoice.Shared.Response;
using Microsoft.AspNetCore.Mvc;

namespace Invoice.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvoiceController : ControllerBase
{
	private readonly IServiceManager _service;

	public InvoiceController(IServiceManager service) => _service = service;

    [HttpPost("{issuerId:guid}")]
	[ServiceFilter(typeof(ValidationFilterAttribute))]
	public async Task<IActionResult> CreateInvoice(Guid issuerId, InvoiceRequest request)
	{
		var invoiceCreated = await _service.InvoiceService.CreateInvoiceAsync(issuerId, request, trackChanges: false);

        return CreatedAtRoute("InvoiceById", new { id = invoiceCreated.Id }, invoiceCreated);
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<List<InvoiceResponse>>> GetInvoices()
    {
        var issuerResponse = await _service.InvoiceService.GetInvoicesAsync(false);

        return Ok(issuerResponse);
    }

    [HttpGet()]
    public async Task<ActionResult<InvoiceResponse>> GetInvoicBySerie([FromQuery]InvoiceParams invoiceParams)
    {
        var issuerResponse = await _service.InvoiceService.GetInvoiceBySerieAsync(invoiceParams.Serie, invoiceParams.SerialNumber, invoiceParams.CorrelativeNumber, trackChanges: false);

        return Ok(issuerResponse);
    }
}