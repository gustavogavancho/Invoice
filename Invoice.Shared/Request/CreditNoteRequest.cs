using System.ComponentModel.DataAnnotations;

namespace Invoice.Shared.Request;

public record CreditNoteRequest
{
    [Required] public DateTime IssueDate { get; set; }
    [Required, MinLength(3), MaxLength(3)] public string UblVersionId { get; set; } = default!;
    [Required, MinLength(3), MaxLength(3)] public string CustomizationId { get; set; } = default!;
    [Required] public NoteDetailRequest CreditNoteDetail { get; set; } = default!;
    [Required] public ReceiverRequest Receiver { get; set; } = default!;
    [Required] public IEnumerable<PaymentTermsRequest> PaymentTerms { get; set; } = default!;
    [Range(0, 9999999999999999.99)] public decimal TaxTotalAmount { get; set; }
    [Range(0, 9999999999999999.99)] public decimal TotalAmount { get; set; }
    [Required] public IEnumerable<TaxSubTotalRequest> TaxSubTotals { get; set; } = default!;
    [Required] public IEnumerable<ProductDetailsRequest> ProductsDetails { get; set; } = default!;
}