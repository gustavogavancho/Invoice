using Invoice.Entities;
using Invoice.Service.Contracts;
using Invoice.Shared.Request;
using Invoice.Shared.Response;
using Microsoft.AspNetCore.Mvc;

namespace Invoice.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SenderController : ControllerBase
{
    private readonly ISenderService _senderService;

    public SenderController(ISenderService senderService)
    {
        _senderService = senderService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSender(SenderDataRequest senderRequest)
    {
        await _senderService.CreateSender(senderRequest);

        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SenderResponse>> GetSender(Guid id)
    {
        var senderResponse = await _senderService.GetSender(id);

        return Ok(senderResponse);
    }

    [HttpGet]
    public async Task<ActionResult<List<SenderResponse>>> GetSenders()
    {
        var senderResponses = await _senderService.GetSenders();

        return Ok(senderResponses);
    }
}