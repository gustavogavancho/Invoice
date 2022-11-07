namespace Invoice.Shared.Response;

public class DocumentsResponse
{
    public string TicketType { get; set; } = default!;
    public bool SummarySended { get; set; }
    public string Ticket { get; set; } = default!;
}
