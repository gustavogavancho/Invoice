using System.ComponentModel.DataAnnotations;

namespace Invoice.Shared.Request;

public class InvoiceDataRequest
{
    [Required, MinLength(2), MaxLength(2)] public string Serie { get; set; } = default!;
    [Range(1, 99)] public uint SerialNumber { get; set; }
    [Range(1, 99999999)] public int CorrelativeNumber { get; set; }
    [Required, MinLength(4), MaxLength(4)] public string OperationType { get; set; } = default!; //Catalog 51
    [Required, MinLength(2), MaxLength(2)] public string DocumentType { get; set; } = default!; //Catalog 1
    [Required, MinLength(4), MaxLength(4)] public string NoteTypeCode { get; set; } = default!; //Catalogo 52
    [Required] public string NoteType { get; set; } = default!; //e. "MONTO EN SOLES"
    [Required, MinLength(3), MaxLength(3)] public string CurrencyCode { get; set; } = default!; //ISO 4217 e. "PEN"
}