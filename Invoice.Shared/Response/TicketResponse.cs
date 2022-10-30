using System.ComponentModel.DataAnnotations.Schema;

namespace Invoice.Shared.Response;

public class TicketResponse
{
    public string TicketNumber { get; set; } = default!;
    public DateTime IssueDate { get; set; }
    public bool Status { get; set; }
    [Column(TypeName = "xml")]
    public string SummaryDocumentsXml { get; set; } = default!;
}