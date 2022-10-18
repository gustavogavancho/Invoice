using System.ComponentModel.DataAnnotations;

namespace Invoice.Shared.Request;

public record InvoiceRequest
{
    [Required] public DateTime IssueDate { get; set; }
    public DateTime? DueDate { get; set; }
    [Required] public UblSchemeRequest UblScheme { get; set; } = default!;
    [Required] public InvoiceDataRequest InvoiceData { get; set; } = default!;
    [Required] public IssuerRequest Issuer { get; set; } = default!;
    [Required] public ReceiverDataRequest ReceiverData { get; set; } = default!;
    [Required] public IEnumerable<PaymentTermsRequest> PaymentTerms { get; set; } = default!;
    [Range(0, 9999999999999999.99)] public decimal TaxTotalAmount { get; set; }
    [Range(0, 9999999999999999.99)] public decimal TotalAmount { get; set; }
    [Required] public IEnumerable<TaxSubTotalRequest> TaxSubTotal { get; set; } = default!;
    [Required] public IEnumerable<ProductsDetailsRequest> ProductsDetail { get; set; } = default!;
}