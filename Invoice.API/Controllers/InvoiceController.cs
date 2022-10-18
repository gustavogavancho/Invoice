using Invoice.Service.Contracts;
using Invoice.Shared.Request;
using Microsoft.AspNetCore.Mvc;

namespace Invoice.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvoiceController : ControllerBase
{
	private readonly IInvoiceService _invoiceSerializerService;

	public InvoiceController(IInvoiceService invoiceSerializerService)
	{
		_invoiceSerializerService = invoiceSerializerService;
	}

	[HttpPost("{id:guid}")]
	public async Task<IActionResult> Send(Guid id, InvoiceRequest request)
	{
		await _invoiceSerializerService.SendInvoiceType(id, request);

		return Ok();
	}
}