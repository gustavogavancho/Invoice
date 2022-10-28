using System.ComponentModel.DataAnnotations;

namespace Invoice.Shared.Request;

public class SummaryDocumentsRequest
{
    [Required] public DateTime IssueDate { get; set; }
    [Required] public DateTime ReferenceDate { get; set; }
    [Required, MinLength(3), MaxLength(3)] public string UblVersionId { get; set; } = default!;
    [Required, MinLength(3), MaxLength(3)] public string CustomizationId { get; set; } = default!;
    [Required] public IEnumerable<TicketsRequest> Tickets { get; set; } = default!;
}