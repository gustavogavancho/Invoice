using Invoice.API.ActionFilters;
using Invoice.Service.Contracts.ServiceManagers;
using Invoice.Shared.Request;
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
	public async Task<IActionResult> Send(Guid id, InvoiceRequest request)
	{
		await _service.InvoiceService.SendInvoiceType(id, request, trackChanges: false);

		return Ok();
	}
}