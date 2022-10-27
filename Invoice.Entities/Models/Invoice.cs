using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Invoice.Entities.Models;

public class Invoice
{
    public Guid Id { get; set; }
    [Column(TypeName = "xml")]
    public string InvoiceXml { get; set; } = default!;
    public byte[] SunatResponse { get; set; } = default!;
    public bool Accepted { get; set; }
    public string Observations { get; set; } = default!;
    public Issuer Issuer { get; set; } = default!;
    public Guid IssuerId { get; set; }
    [Required] public DateTime IssueDate { get; set; }
    [Required, MinLength(3), MaxLength(3)] public string UblVersionId { get; set; } = default!;
    [Required, MinLength(3), MaxLength(3)] public string CustomizationId { get; set; } = default!;
    [Required] public InvoiceDetail InvoiceDetail { get; set; } = default!;
    [Required] public Receiver Receiver { get; set; } = default!;
    [Required] public IEnumerable<PaymentTerms> PaymentTerms { get; set; } = default!;
    [Range(0, 9999999999999999.99)] public decimal TaxTotalAmount { get; set; }
    [Range(0, 9999999999999999.99)] public decimal TotalAmount { get; set; }
    [Required] public IEnumerable<TaxSubTotal> TaxSubTotals { get; set; } = default!;
    [Required] public IEnumerable<ProductDetails> ProductsDetails { get; set; } = default!;
    public bool SummaryApproved { get; set; }
    public string? SummaryObservations { get; set; }
    public bool Canceled { get; set; }
    public string? CanceledReason { get; set; }
}