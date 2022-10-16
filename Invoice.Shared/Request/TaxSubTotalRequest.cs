using System.ComponentModel.DataAnnotations;

namespace Invoice.Shared.Request;

public class TaxSubTotalRequest
{
    [Required, Range(0, 9999999999999999.99)] public decimal TaxableAmount { get; set; }
    [Required, Range(0, 9999999999999999.99)] public decimal TaxAmount { get; set; }
    [Required] public TaxCategoryRequest TaxCategory { get; set; } = default!;
}