using System.ComponentModel.DataAnnotations;

namespace Invoice.Shared.Request;

public class ProductsDetailsRequest
{
    [Required] public string UnitCode { get; set; } = default!; //Sunat UNIT CODES
    [Required, Range(0, 9999999999999999.99)] public decimal Quantity { get; set; }
    [Required, Range(0, 9999999999999999.99)] public decimal UnitPrice { get; set; }
    [Required, Range(0, 9999999999999999.99)] public decimal TaxAmount { get; set; }
    [Required, MinLength(2), MaxLength(2)] public string PriceType { get; set; } = default!; //Catalog 16
    [Required, Range(0, 99)] public decimal TaxPercentage { get; set; }
    [Required, MaxLength(2)] public string TaxExemptionReasonCode { get; set; } = default!; //Catalog 7
    [Required, MinLength(4), MaxLength(4)] public string TaxId { get; set; } = default!; //Catalog 5
    [Required, MinLength(3), MaxLength(4)] public string TaxName { get; set; } = default!; //Catalog 5
    [Required, MinLength(3), MaxLength(4)] public string TaxCode { get; set; } = default!; //Catalog 5
    [Required] public string SellerItemIdentification { get; set; } = default!; //Internal product code
    [Required] public string ItemClassificationCode { get; set; } = default!; //Codigo estandar 25172405
    [Required] public string Description { get; set; } = default!;
}