using Invoice.Service.Contracts.BusinessServices;
using Invoice.Shared.Request;
using Invoice.Shared.Response;
using Microsoft.AspNetCore.Mvc;

namespace Invoice.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IssuerController : ControllerBase
{
    private readonly IIssuerService _issuerService;

    public IssuerController(IIssuerService issuerService)
    {
        _issuerService = issuerService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateIssuer(IssuerRequest issuerRequest)
    {
        await _issuerService.CreateIssuer(issuerRequest);

        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteIssuer(Guid id)
    {
        await _issuerService.DeleteIssuer(id);

        return Ok();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<IssuerResponse>> GetIssuer(Guid id)
    {
        var issuerResponse = await _issuerService.GetIssuer(id);

        return Ok(issuerResponse);
    }

    [HttpGet]
    public async Task<ActionResult<List<IssuerResponse>>> GetIssuers()
    {
        var issuerResponses = await _issuerService.GetIssuers();

        return Ok(issuerResponses);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateIssuer(Guid id, IssuerRequest issuerRequest)
    {
        await _issuerService.UpdateIssuer(id, issuerRequest);

        return NoContent();
    }
}