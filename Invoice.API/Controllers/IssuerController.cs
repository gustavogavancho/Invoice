using Invoice.API.ActionFilters;
using Invoice.Service.Contracts.ServiceManagers;
using Invoice.Shared.Request;
using Invoice.Shared.Response;
using Microsoft.AspNetCore.Mvc;

namespace Invoice.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IssuerController : ControllerBase
{
    private readonly IServiceManager _service;

    public IssuerController(IServiceManager service) => _service = service;

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateIssuer(IssuerRequest issuerRequest)
    {
        var issuerCreated = await _service.IssuerService.CreateIssuerAsync(issuerRequest);

        return CreatedAtRoute("IssuerById", new { id = issuerCreated.Id }, issuerCreated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteIssuer(Guid id)
    {
        await _service.IssuerService.DeleteIssuerAsync(id, trackChanges: false);

        return NoContent();
    }

    [HttpGet("{id:guid}", Name = "IssuerById")]
    public async Task<ActionResult<IssuerResponse>> GetIssuer(Guid id)
    {
        var issuerResponse = await _service.IssuerService.GetIssuerAsync(id, trackChanges: false);
        return Ok(issuerResponse);
    }

    [HttpGet(Name = "GetIssuers")]
    public async Task<ActionResult<List<IssuerResponse>>> GetIssuers()
    {
        var issuerResponses = await _service.IssuerService.GetIssuersAsync(trackChanges: false);

        return Ok(issuerResponses);
    }

    [HttpPut("{id:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateIssuer(Guid id, IssuerRequest issuerRequest)
    {
        await _service.IssuerService.UpdateIssuerAsync(id, issuerRequest, trackChanges: true);

        return NoContent();
    }
}