using System.ComponentModel.DataAnnotations;

namespace Invoice.Shared.Request;

public record TaxCategoryRequest
{
    [Required, MinLength(4), MaxLength(4)] public string TaxId { get; set; } = default!; //Catalog 5
    [Required, MinLength(3), MaxLength(4)] public string TaxName { get; set; } = default!; //Catalog 5
    [Required, MinLength(3), MaxLength(4)] public string TaxCode { get; set; } = default!; //Catalog 5
}