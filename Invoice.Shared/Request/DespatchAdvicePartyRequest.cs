using System.ComponentModel.DataAnnotations;

namespace Invoice.Shared.Request;

public record DespatchAdvicePartyRequest
{
    public ulong DespatchAdvicePartyId { get; set; } //RUC 
    [Required] public string DespatchAdvicePartyName { get; set; } = default!;
    [Required, MinLength(1), MaxLength(1)] public string DespatchAdvicePartyType { get; set; } = default!; //Catalog 6
}
