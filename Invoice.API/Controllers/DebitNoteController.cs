using Invoice.API.ActionFilters;
using Invoice.Service.Contracts.ServiceManagers;
using Invoice.Shared.Request;
using Microsoft.AspNetCore.Mvc;

namespace Invoice.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DebitNoteController : ControllerBase
{
    private readonly IServiceManager _service;

    public DebitNoteController(IServiceManager service) => _service = service;

    [HttpPost("{issuerId:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateDebitNote(Guid issuerId, DebitNoteRequest request)
    {
        var invoiceCreated = await _service.InvoiceService.CreateDebitNoteAsync(issuerId, request, trackChanges: false);

        return Ok(invoiceCreated);
    }
}