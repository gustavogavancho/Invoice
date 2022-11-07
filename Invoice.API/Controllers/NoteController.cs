using Invoice.API.ActionFilters;
using Invoice.Service.Contracts.ServiceManagers;
using Invoice.Shared.Request;
using Invoice.Shared.Response;
using Microsoft.AspNetCore.Mvc;

namespace Invoice.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NoteController : ControllerBase
{
    private readonly IServiceManager _service;

    public NoteController(IServiceManager service) => _service = service;

    [HttpPost("CreditNote/{issuerId:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateCreditNote(Guid issuerId, NoteRequest request)
    {
        var debitNoteCreated = await _service.CreditNoteService.CreateCreditNoteAsync(issuerId, request, trackChanges: false);

        return CreatedAtRoute("CreditNoteById", new { id = debitNoteCreated.Id }, debitNoteCreated);
    }

    [HttpGet("CreditNote/{id:guid}", Name = "CreditNoteById")]
    public async Task<ActionResult<InvoiceResponse>> GetCreditNote(Guid id)
    {
        var creditNoteResponse = await _service.CreditNoteService.GetCreditNoteAsync(id, trackChanges: false);

        return Ok(creditNoteResponse);
    }

    [HttpPost("DebitNote/{issuerId:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateDebitNote(Guid issuerId, NoteRequest request)
    {
        var debitNoteCreated = await _service.DebitNoteService.CreateDebitNoteAsync(issuerId, request, trackChanges: false);

        return CreatedAtRoute("DebitNoteById", new { id = debitNoteCreated.Id }, debitNoteCreated);
    }

    [HttpGet("DebitNote/{id:guid}", Name = "DebitNoteById")]
    public async Task<ActionResult<InvoiceResponse>> GetDebitNote(Guid id)
    {
        var debitNoteResponse = await _service.DebitNoteService.GetDebitNoteAsync(id, trackChanges: false);

        return Ok(debitNoteResponse);
    }
}