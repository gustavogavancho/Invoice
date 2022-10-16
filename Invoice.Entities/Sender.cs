using System.ComponentModel.DataAnnotations;

namespace Invoice.Entities;

public class Sender
{
    public Guid Id { get; set; }
    [Range(10000000000, 99999999999)] public ulong SenderId { get; set; } //RUC 
    [Required] public string SenderName { get; set; } = default!;
    [Required, MinLength(1), MaxLength(1)] public string SenderType { get; set; } = default!; //Catalog 6
    [Required, MinLength(6), MaxLength(6)] public string GeoCode { get; set; } = default!; //Ubigeo
    [Required, MinLength(4), MaxLength(4)] public string EstablishmentCode { get; set; } = default!; //e. "0000"
    [Required] public string Department { get; set; } = default!;
    [Required] public string Province { get; set; } = default!;
    [Required] public string District { get; set; } = default!;
    [Required] public string Address { get; set; } = default!;
}