using System.ComponentModel.DataAnnotations;

namespace Invoice.Entities.Models;

public class InvoiceTaxSubTotal
{
    public Guid Id { get; set; }
    [Range(0, 9999999999999999.99)] public decimal TaxableAmount { get; set; }
    [Range(0, 9999999999999999.99)] public decimal TaxAmount { get; set; }
    [MinLength(4), MaxLength(4)] public string TaxId { get; set; } = default!; //Catalog 5
    [MinLength(3), MaxLength(4)] public string TaxName { get; set; } = default!; //Catalog 5
    [MinLength(3), MaxLength(4)] public string TaxCode { get; set; } = default!; //Catalog 5S
    public Guid InvoiceId { get; set; }
}