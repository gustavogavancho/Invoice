using System.ComponentModel.DataAnnotations;

namespace Invoice.Shared.Request;

public record ReceiverDataRequest
{
    [Range(10000000000, 99999999999)] public ulong ReceiverId { get; set; } //RUC 
    [Required] public string ReceiverName { get; set; } = default!;
    [Required, MinLength(1), MaxLength(1)] public string ReceiverType { get; set; } = default!; //Catalog 6
    [Required] public string FullAddress { get; set; } = default!;
}