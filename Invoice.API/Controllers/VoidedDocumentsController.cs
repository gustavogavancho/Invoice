using Invoice.API.ActionFilters;
using Invoice.Service.Contracts.ServiceManagers;
using Invoice.Shared.Request;
using Microsoft.AspNetCore.Mvc;

namespace Invoice.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VoidedDocumentsController : ControllerBase
{
    private readonly IServiceManager _service;

    public VoidedDocumentsController(IServiceManager service) => _service = service;

    [HttpPost("{issuerId:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateVoidedDocuments(Guid issuerId, VoidedDocumentsRequest request)
    {
        var summaryDocumentsCreated = await _service.VoidedDocumentsService.CreateVoidedDocumentsAsync(issuerId, request, trackChanges: true);

        return Ok(summaryDocumentsCreated);
    }

}