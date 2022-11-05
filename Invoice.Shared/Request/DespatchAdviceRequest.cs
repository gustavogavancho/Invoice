using System.ComponentModel.DataAnnotations;

namespace Invoice.Shared.Request;

public class DespatchAdviceRequest
{
    [Required] public DateTime IssueDate { get; set; }
    [Required, MinLength(3), MaxLength(3)] public string UblVersionId { get; set; } = default!;
    [Required, MinLength(3), MaxLength(3)] public string CustomizationId { get; set; } = default!;
    [Required] public DespatchAdviceDetailRequest DespatchAdviceDetail { get; set; } = default!;
    [Required] public ReceiverRequest Receiver { get; set; } = default!;
    [Required] public DeliveryCustomerRequest DeliveryCustomer { get; set; } = default!;
    [Required] public SellerSupplierRequest SellerSupplier { get; set; } = default!;
    [Required] public ShipmentRequest Shipment { get; set; } = default!;
    [Required] public IEnumerable<ProductDespatchDetailsRequest> ProductsDetails { get; set; } = default!;
}