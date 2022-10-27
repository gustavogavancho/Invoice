using Invoice.API.ActionFilters;
using Invoice.Service.Contracts.ServiceManagers;
using Invoice.Shared.Request;
using Invoice.Shared.Response;
using Microsoft.AspNetCore.Mvc;

namespace Invoice.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CreditNoteController : ControllerBase
{
    private readonly IServiceManager _service;

    public CreditNoteController(IServiceManager service) => _service = service;

    [HttpPost("{issuerId:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateCreditNote(Guid issuerId, CreditNoteRequest request)
    {
        var debitNoteCreated = await _service.CreditNoteService.CreateCreditNoteAsync(issuerId, request, trackChanges: false);

        return CreatedAtRoute("CreditNoteById", new { id = debitNoteCreated.Id }, debitNoteCreated);
    }

    [HttpGet("{id:guid}", Name = "CreditNoteById")]
    public async Task<ActionResult<CreditNoteResponse>> GetCreditNote(Guid id)
    {
        var creditNoteResponse = await _service.CreditNoteService.GetCreditNoteAsync(id, trackChanges: false);

        return Ok(creditNoteResponse);
    }
}