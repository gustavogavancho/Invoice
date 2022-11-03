using System.ComponentModel.DataAnnotations;

namespace Invoice.Shared.Request;

public class VoidedDocumentsRequest
{
    [Range(0, 999)] public int VoidedDocumentsId { get; set; }
    [Required] public DateTime IssueDate { get; set; }
    [Required] public DateTime ReferenceDate { get; set; }
    [Required, MinLength(3), MaxLength(3)] public string UblVersionId { get; set; } = default!;
    [Required, MinLength(3), MaxLength(3)] public string CustomizationId { get; set; } = default!;
    [Required] public IEnumerable<DocumentToVoidRequest> DocumentsToVoid { get; set; } = default!;
}