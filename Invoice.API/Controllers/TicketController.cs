using Invoice.Service.Contracts.ServiceManagers;
using Invoice.Shared.Response;
using Microsoft.AspNetCore.Mvc;

namespace Invoice.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketController : ControllerBase
{
    private readonly IServiceManager _service;

    public TicketController(IServiceManager service) => _service = service;

    [HttpGet("{ticketNumber}", Name = "TicketByNumber")]
    public async Task<ActionResult<TicketResponse>> GetTicket(string ticketNumber)
    {
        var ticketResponse = await _service.TicketService.GetTicketAsync(ticketNumber, trackChanges: false);

        return Ok(ticketResponse);
    }
}