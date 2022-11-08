using System.ComponentModel.DataAnnotations;

namespace Invoice.Shared.Request;

public record DespatchAdviceRequest
{
    [Required] public DateTime IssueDate { get; set; }
    [Required, MinLength(3), MaxLength(3)] public string UblVersionId { get; set; } = default!;
    [Required, MinLength(3), MaxLength(3)] public string CustomizationId { get; set; } = default!;
    [Required] public DespatchDetailRequest DespatchAdviceDetail { get; set; } = default!;
    [Required] public DespatchPartyRequest DeliveryCustomer { get; set; } = default!;
    [Required] public DespatchPartyRequest SellerSupplier { get; set; } = default!;
    [Required] public DespatchShipmentRequest Shipment { get; set; } = default!;
    [Required] public IEnumerable<DespatchProductDetailsRequest> ProductsDetails { get; set; } = default!;
}