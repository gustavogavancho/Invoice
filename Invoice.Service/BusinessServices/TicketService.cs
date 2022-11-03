using AutoMapper;
using Invoice.Contracts.Logger;
using Invoice.Contracts.Repositories;
using Invoice.Entities.Exceptions;
using Invoice.Entities.Models;
using Invoice.Service.Contracts.BusinessServices;
using Invoice.Service.Contracts.HelperServices;
using Invoice.Shared.Response;

namespace Invoice.Service.BusinessServices;

public class TicketService : ITicketService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IDocumentGeneratorService _documentGeneratorService;
    private readonly ISunatService _sunatService;

    public TicketService(IRepositoryManager repository,
        ILoggerManager logger,
        IMapper mapper,
        IDocumentGeneratorService documentGeneratorService,
        ISunatService sunatService)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
        _documentGeneratorService = documentGeneratorService;
        _sunatService = sunatService;
    }

    public async Task<TicketResponse> GetTicketAsync(string ticketNumber, bool trackChanges)
    {
        var ticket = await GetTciketAndCheckIfItExists(ticketNumber, trackChanges);

        var ticketResponse = _mapper.Map<Ticket, TicketResponse>(ticket);

        return ticketResponse;
    }

    public async Task<TicketResponse> GetTicketStatusAsync(string ticketNumber, bool trackChanges)
    {
        var ticket = await GetTciketAndCheckIfItExists(ticketNumber, trackChanges);

        var ticketStatusResponse = await _sunatService.GetStatus("https://e-beta.sunat.gob.pe/ol-ti-itcpfegem-beta/billService",
                "20606022779MODDATOS",
                "moddatos",
                ticket.TicketNumber);

        ticket.StatusCode = ticketStatusResponse.Item1;
        ticket.StatusContent = ticketStatusResponse.Item2;

        if (ticket.StatusCode == "0")
        {
            ticket.Status = true;

            //Read response
            var responses = _sunatService.ReadResponse(ticket.StatusContent);

            var invoices = await _repository.Invoice.GetTicketsByIssueDateAsync(ticket.IssueDate, false, true);
            foreach (var invoice in invoices)
            {
                invoice.SummaryStatus = true;
                invoice.CanceledReason = string.Join("|", responses);
            }
        }

        await _repository.SaveAsync();

        var ticketResponse = _mapper.Map<Ticket, TicketResponse>(ticket);

        return ticketResponse;
    }

    private async Task<Ticket> GetTciketAndCheckIfItExists(string ticketNumber, bool trackChanges)
    {
        var ticket = await _repository.Ticket.GetTicketAsync(ticketNumber, trackChanges);

        if (ticket is null)
            throw new TicketNotFoundException(ticketNumber);

        return ticket;
    }
}