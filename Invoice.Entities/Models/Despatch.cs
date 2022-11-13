using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Invoice.Shared.Request;

namespace Invoice.Entities.Models;

public class Despatch
{
    public Guid Id { get; set; }
    [Column(TypeName = "xml")]
    public string DespatchXml { get; set; } = default!;
    public byte[] SunatResponse { get; set; } = default!;
    public bool Accepted { get; set; }
    public string Observations { get; set; } = default!;
    public Issuer Issuer { get; set; } = default!;
    public Guid IssuerId { get; set; }
    [Required] public DateTime IssueDate { get; set; }
    [Required, MinLength(3), MaxLength(3)] public string UblVersionId { get; set; } = default!;
    [Required, MinLength(3), MaxLength(3)] public string CustomizationId { get; set; } = default!;
    [Required] public DespatchDetail DespatchDetail { get; set; } = default!;
    [Required] public DespatchDeliveryCustomer DeliveryCustomer { get; set; } = default!;
    [Required] public DespatchSellerSupplier SellerSupplier { get; set; } = default!;
    [Required] public DespatchShipment Shipment { get; set; } = default!;
    [Required] public IEnumerable<DespatchProductDetails> ProductsDetails { get; set; } = default!;
}