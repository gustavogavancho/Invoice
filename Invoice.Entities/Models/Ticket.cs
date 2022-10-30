using System.ComponentModel.DataAnnotations.Schema;

namespace Invoice.Entities.Models;

public class Ticket
{
    public Guid Id { get; set; }
    public string TicketNumber { get; set; } = default!;
    public DateTime IssueDate { get; set; }
    public bool Status { get; set; }
    [Column(TypeName = "xml")]
    public string SummaryDocumentsXml { get; set; } = default!;
}