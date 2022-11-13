using Invoice.API.ActionFilters;
using Invoice.Service.Contracts.ServiceManagers;
using Invoice.Shared.Params;
using Invoice.Shared.Request;
using Invoice.Shared.Response;
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

    [HttpGet("GetAll")]
    public async Task<ActionResult<List<DespatchResponse>>> GetDespatches()
    {
        var despatchesResponse = await _service.DespatchAdviceService.GetDespatchesAsync(false);

        return Ok(despatchesResponse);
    }

    [HttpGet()]
    public async Task<ActionResult<DespatchResponse>> GetDespatchBySerie([FromQuery] DespatchParams despatchParams)
    {
        var despatchResponse = await _service.DespatchAdviceService.GetDespatchAdviceBySerieAsync(despatchParams.Serie, despatchParams.SerialNumber, despatchParams.CorrelativeNumber, trackChanges: false);

        return Ok(despatchResponse);
    }
}