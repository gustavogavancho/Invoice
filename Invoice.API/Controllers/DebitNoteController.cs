using Invoice.API.ActionFilters;
using Invoice.Entities.Models;
using Invoice.Service.Contracts.ServiceManagers;
using Invoice.Shared.Request;
using Invoice.Shared.Response;
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
    public async Task<IActionResult> CreateDebitNote(Guid issuerId, NoteRequest request)
    {
        var debitNoteCreated = await _service.DebitNoteService.CreateDebitNoteAsync(issuerId, request, trackChanges: false);

        return CreatedAtRoute("DebitNoteById", new { id = debitNoteCreated.Id }, debitNoteCreated);
    }

    [HttpGet("{id:guid}", Name = "DebitNoteById")]
    public async Task<ActionResult<InvoiceResponse>> GetDebitNote(Guid id)
    {
        var debitNoteResponse = await _service.DebitNoteService.GetDebitNoteAsync(id, trackChanges: false);

        return Ok(debitNoteResponse);
    }
}