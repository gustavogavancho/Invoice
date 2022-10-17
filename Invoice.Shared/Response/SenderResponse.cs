using System.ComponentModel.DataAnnotations;

namespace Invoice.Shared.Response;

public record SenderResponse
{
    public Guid Id { get; set; }
    public ulong SenderId { get; set; } //RUC 
    public string SenderName { get; set; } = default!;
    public string SenderType { get; set; } = default!; //Catalog 6
    public string GeoCode { get; set; } = default!; //Ubigeo
    public string EstablishmentCode { get; set; } = default!; //e. "0000"
    public string Department { get; set; } = default!;
    public string Province { get; set; } = default!;
    public string District { get; set; } = default!;
    public string Address { get; set; } = default!;
}