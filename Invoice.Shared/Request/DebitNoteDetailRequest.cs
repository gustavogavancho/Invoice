using System.ComponentModel.DataAnnotations;

namespace Invoice.Shared.Request;

public class DebitNoteDetailRequest
{
    [Required, MinLength(2), MaxLength(2)] public string Serie { get; set; } = default!;
    [Range(1, 99)] public uint SerialNumber { get; set; }
    [Range(1, 99999999)] public int CorrelativeNumber { get; set; }
    [Required, MinLength(4), MaxLength(4)] public string OperationType { get; set; } = default!; //Catalog 51
    [Required, MinLength(2), MaxLength(2)] public string DocumentType { get; set; } = default!; //Catalog 1
    [Required, MinLength(3), MaxLength(3)] public string CurrencyCode { get; set; } = default!; //ISO 4217 e. "PEN"
    [Required, MinLength(2), MaxLength(2)] public string InvoiceSerie { get; set; } = default!;
    [Range(1, 99)] public uint InvoiceSerialNumber { get; set; }
    [Range(1, 99999999)] public int InvoiceCorrelativeNumber { get; set; }
    [Required, MinLength(2), MaxLength(2)] public string InvoiceDocumentType { get; set; } = default!; //Catalog 1
    [Required, MinLength(2), MaxLength(2)] public string ResponseCode { get; set; } = default!; //Catalog 9
    [Required] public string ResponseCodeDescription { get; set; } = default!;
}