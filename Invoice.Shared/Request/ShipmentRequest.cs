using System.ComponentModel.DataAnnotations;

namespace Invoice.Shared.Request;

public class ShipmentRequest
{
    public int IdNumber { get; set; }
    [Required, MinLength(2), MaxLength(2)] public string HandlingCode { get; set; } = default!;
    [Required] public string Information { get; set; } = default!;
    public decimal GrossWeightMeasure { get; set; }
    [Required] public string UnitCode { get; set; } = default!;
    public decimal TotalTransportHandlingUnitQuantity { get; set; } = default!;
    public bool SplitConsignmentIndicator { get; set; }
    public DateTime TransitPeriod { get; set; }
    [Required, MinLength(1), MaxLength(2)] public string CarrierPartyType { get; set; } = default!;
    public int CarrierPartyId { get; set; }
    [Required] public string CarrierPartyName { get; set; } = default!;
    [Required] public string TransportLicensePlate { get; set; } = default!;
    [Required, MinLength(1), MaxLength(1)] public string DriverIdType { get; set; } = default!;
    public int DriveId { get; set; }
    [Required, MinLength(6), MaxLength(6)] public string DeliveryGeoCode { get; set; } = default!;
    [Required] public string DeliveryAddress { get; set; } = default!;
    [Required] public string TransportHandlingUnit { get; set; } = default!;
    [Required, MinLength(6), MaxLength(6)] public string OriginGeoCode { get; set; } = default!;
    [Required] public string OriginAddress { get; set; } = default!;
    [Required] public string FirstArrivalPortLocation { get; set; } = default!;
}