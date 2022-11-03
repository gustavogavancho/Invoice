namespace Invoice.Shared.Response;

public class VoidedDocumentsResponse
{
    public string TicketType { get; set; } = default!;
    public bool SummarySended { get; set; }
    public string Ticket { get; set; } = default!;
}