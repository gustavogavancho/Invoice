using System.ComponentModel.DataAnnotations;

namespace Invoice.Entities.Models;

public class InvoiceDetail
{
    public Guid Id { get; set; }
    [Required, MinLength(2), MaxLength(2)] public string Serie { get; set; } = default!;
    [Range(1, 99)] public uint SerialNumber { get; set; }
    [Range(1, 99999999)] public int CorrelativeNumber { get; set; }
    [MinLength(4), MaxLength(4)] public string OperationType { get; set; } = default!; //Catalog 51
    [MinLength(2), MaxLength(2)] public string DocumentType { get; set; } = default!; //Catalog 1
    [MinLength(4), MaxLength(4)] public string? NoteTypeCode { get; set; } //Catalogo 52
    public string? NoteType { get; set; } //e. "MONTO EN SOLES"
    [MinLength(3), MaxLength(3)] public string CurrencyCode { get; set; } = default!; //ISO 4217 e. "PEN"
    public Guid InvoiceId { get; set; }
}