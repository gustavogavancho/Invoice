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

    [HttpPost("{issuerId:guid}")]
	[ServiceFilter(typeof(ValidationFilterAttribute))]
	public async Task<IActionResult> CreateInvoice(Guid issuerId, InvoiceRequest request)
	{
		var invoiceCreated = await _service.InvoiceService.CreateInvoiceAsync(issuerId, request, trackChanges: false);

        return CreatedAtRoute("InvoiceById", new { id = invoiceCreated.Id }, invoiceCreated);
    }

    [HttpGet("{invoiceId:guid}", Name = "InvoiceById")]
    public async Task<ActionResult<InvoiceResponse>> GetInvoice(Guid invoiceId)
    {
        var issuerResponse = await _service.InvoiceService.GetInvoiceAsync(invoiceId, trackChanges: false);

        return Ok(issuerResponse);
    }
}