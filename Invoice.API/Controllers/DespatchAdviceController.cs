using Invoice.API.ActionFilters;
using Invoice.Service.Contracts.ServiceManagers;
using Invoice.Shared.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Invoice.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DespatchAdviceController : ControllerBase
{
    private readonly IServiceManager _service;

    public DespatchAdviceController(IServiceManager service) => _service = service;

    [HttpPost("{issuerId:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateDespatchAdvice(Guid issuerId, DespatchRequest request)
    {
        var despathAdviceCreated = await _service.DespatchAdviceService.CreateDespatchAdviceAsync(issuerId, request, trackChanges: false);

        return Ok(despathAdviceCreated);
    }
}