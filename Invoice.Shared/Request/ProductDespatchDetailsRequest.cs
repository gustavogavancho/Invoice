using System.ComponentModel.DataAnnotations;

namespace Invoice.Shared.Request;

public class ProductDespatchDetailsRequest
{
    [Required] public string UnitCode { get; set; } = default!; //Sunat UNIT CODES
    [Range(0, 9999999999999999.99)] public decimal Quantity { get; set; }
    [Required] public string SellerItemIdentification { get; set; } = default!; //Internal product code
    [Required] public string ItemClassificationCode { get; set; } = default!; //Codigo estandar 25172405
    [Required] public string Description { get; set; } = default!;
}