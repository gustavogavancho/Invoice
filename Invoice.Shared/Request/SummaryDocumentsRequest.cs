using System.ComponentModel.DataAnnotations;

namespace Invoice.Shared.Request;

public record SummaryDocumentsRequest
{
    [Range(0, 99999)]public int SummaryDocumentsId { get; set; }
    [Required] public DateTime IssueDate { get; set; }
    [Required] public DateTime ReferenceDate { get; set; }
    [Required, MinLength(3), MaxLength(3)] public string UblVersionId { get; set; } = default!;
    [Required, MinLength(3), MaxLength(3)] public string CustomizationId { get; set; } = default!;
}