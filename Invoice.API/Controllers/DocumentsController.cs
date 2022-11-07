using Invoice.API.ActionFilters;
using Invoice.Service.Contracts.ServiceManagers;
using Invoice.Shared.Request;
using Microsoft.AspNetCore.Mvc;

namespace Invoice.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DocumentsController : ControllerBase
{
    private readonly IServiceManager _service;

    public DocumentsController(IServiceManager service) => _service = service;

    [HttpPost("Summary/{issuerId:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateSummaryDocuments(Guid issuerId, SummaryDocumentsRequest request)
    {
        var summaryDocumentsCreated = await _service.SummaryDocumentsService.CreateSummaryDocumentsAsync(issuerId, request, trackChanges: true);

        return Ok(summaryDocumentsCreated);
    }

    [HttpPost("Voided/{issuerId:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateVoidedDocuments(Guid issuerId, VoidedDocumentsRequest request)
    {
        var summaryDocumentsCreated = await _service.VoidedDocumentsService.CreateVoidedDocumentsAsync(issuerId, request, trackChanges: true);

        return Ok(summaryDocumentsCreated);
    }
}