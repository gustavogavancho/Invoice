using System.ComponentModel.DataAnnotations;

namespace Invoice.Shared.Request;

public record DespatchPartyRequest
{
    public ulong DespatchPartyId { get; set; } //RUC 
    [Required] public string DespatchPartyName { get; set; } = default!;
    [Required, MinLength(1), MaxLength(1)] public string DespatchPartyType { get; set; } = default!; //Catalog 6
}
