using System.ComponentModel.DataAnnotations;

namespace Invoice.Shared.Request;

public class DocumentToVoid
{
    [Required, MinLength(2), MaxLength(2)] public string Serie { get; set; } = default!;
    [Range(1, 99)] public uint SerialNumber { get; set; }
    [Range(1, 99999999)] public int CorrelativeNumber { get; set; }
    [Required, MinLength(2), MaxLength(2)] public string DocumentType { get; set; } = default!; //Catalog 1
    [Required] public string VoidReason { get; set; } = default!;
}