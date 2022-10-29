using Invoice.API.ActionFilters;
using Invoice.Service.Contracts.ServiceManagers;
using Invoice.Shared.Request;
using Microsoft.AspNetCore.Mvc;

namespace Invoice.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SummaryDocumentsController : ControllerBase
{
    private readonly IServiceManager _service;

    public SummaryDocumentsController(IServiceManager service) => _service = service;

    [HttpPost("{issuerId:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateSummaryDocuments(Guid issuerId, SummaryDocumentsRequest request)
    {
        var summaryDocumentsCreated = await _service.SummaryDocumentsService.CreateSummaryDocumentsAsync(issuerId, request, trackChanges: true);

        return Ok(summaryDocumentsCreated);
    }
}