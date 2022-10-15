using Invoice.Service.Contracts;
using Invoice.Shared.Request;
using Microsoft.AspNetCore.Mvc;

namespace Invoice.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvoiceController : ControllerBase
{
	private readonly IInvoiceSerializerService _invoiceSerializerService;

	public InvoiceController(IInvoiceSerializerService invoiceSerializerService)
	{
		_invoiceSerializerService = invoiceSerializerService;
	}

	[HttpPost("Send")]
	public IActionResult Send(InvoiceRequest request)
	{
		_invoiceSerializerService.SerializeInvoiceType(request);

		return Ok();
	}
}