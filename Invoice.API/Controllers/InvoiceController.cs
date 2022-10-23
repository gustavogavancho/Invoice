using Invoice.API.ActionFilters;
using Invoice.Service.Contracts.ServiceManagers;
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

	[HttpPost("{id:guid}")]
	[ServiceFilter(typeof(ValidationFilterAttribute))]
	public async Task<IActionResult> CreateInvoice(Guid id, InvoiceRequest request)
	{
		var invoiceCreated = await _service.InvoiceService.SendInvoiceType(id, request, trackChanges: false);

        return CreatedAtRoute("InvoiceById", new { id = invoiceCreated.Id }, invoiceCreated);
    }

    [HttpGet("{id:guid}", Name = "InvoiceById")]
    public async Task<ActionResult<InvoiceResponse>> GetInvoice(Guid id)
    {
        var issuerResponse = await _service.InvoiceService.GetInvoiceAsync(id, trackChanges: false);
        return Ok(issuerResponse);
    }
}