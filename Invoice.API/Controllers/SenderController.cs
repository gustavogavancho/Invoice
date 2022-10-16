using Invoice.Service.Contracts;
using Invoice.Shared.Request;
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
}