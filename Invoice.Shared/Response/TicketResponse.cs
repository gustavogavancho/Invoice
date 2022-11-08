using System.ComponentModel.DataAnnotations.Schema;

namespace Invoice.Shared.Response;

public class TicketResponse
{
    public string TicketNumber { get; set; } = default!;
    public DateTime IssueDate { get; set; }
    [Column(TypeName = "xml")]
    public string DocumentsXml { get; set; } = default!;
    public string StatusCode { get; set; } = default!;
    public byte[] StatusContent { get; set; } = default!;
}