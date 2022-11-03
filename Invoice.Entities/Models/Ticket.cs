using System.ComponentModel.DataAnnotations.Schema;

namespace Invoice.Entities.Models;

public class Ticket
{
    public Guid Id { get; set; }
    public string TicketType { get; set; } = default!;
    public string TicketNumber { get; set; } = default!;
    public string? TicketResponse { get; set; }
    public DateTime IssueDate { get; set; }
    [Column(TypeName = "xml")]
    public string DocumentsXml { get; set; } = default!;
    public string? StatusCode { get; set; }
    public byte[]? StatusContent { get; set; } 
}