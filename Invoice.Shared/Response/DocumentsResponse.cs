namespace Invoice.Shared.Response;

public record DocumentsResponse
{
    public string TicketType { get; set; } = default!;
    public bool SummarySended { get; set; }
    public string Ticket { get; set; } = default!;
}
