using System.ComponentModel.DataAnnotations;

namespace Invoice.Shared.Request;

public record SellerSupplierRequest
{
    public ulong SellerSupplierId { get; set; } //RUC 
    [Required] public string SellerSupplierName { get; set; } = default!;
    [Required, MinLength(1), MaxLength(1)] public string SellerSuplierType { get; set; } = default!; //Catalog 6
}