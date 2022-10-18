namespace Invoice.Shared.Response;

public record IssuerResponse
{
    public Guid Id { get; set; }
    public ulong IssuerId { get; set; } //RUC 
    public string IssuerName { get; set; } = default!;
    public string IssuerType { get; set; } = default!; //Catalog 6
    public string GeoCode { get; set; } = default!; //Ubigeo
    public string EstablishmentCode { get; set; } = default!; //e. "0000"
    public string Department { get; set; } = default!;
    public string Province { get; set; } = default!;
    public string District { get; set; } = default!;
    public string Address { get; set; } = default!;
}