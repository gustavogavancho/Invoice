using System.ComponentModel.DataAnnotations;

namespace Invoice.Shared.Request;

public class TicketsRequest
{
    [Required, MinLength(2), MaxLength(2)] public string DocumentType { get; set; } = default!; //Catalog 1
    [Required, MinLength(13), MaxLength(13)] public string TicketId { get; set; } = default!; 
    public ulong ReceiverId { get; set; } //RUC - DNI
    [Required, MinLength(1), MaxLength(1)] public string ReceiverType { get; set; } = default!; //Catalog 6
    [Required, MinLength(1), MaxLength(1)] public string StatusCoditionCode { get; set; } = default!; //Catalog 19
    [Required, MinLength(3), MaxLength(3)] public string CurrencyCode { get; set; } = default!; //ISO 4217 e. "PEN"
    [Range(0, 9999999999999999.99)] public decimal TotalAmount { get; set; }
    [Range(0, 9999999999999999.99)] public decimal PaidAmount { get; set; }
    [Range(0, 9999999999999999.99)] public decimal TaxAmount { get; set; }
    [Required, MinLength(2), MaxLength(2)] public string InstructionId { get; set; } = default!; //Catalog 11
    [Required, MinLength(4), MaxLength(4)] public string TaxId { get; set; } = default!; //Catalog 5
    [Required, MinLength(3), MaxLength(4)] public string TaxName { get; set; } = default!; //Catalog 5
    [Required, MinLength(3), MaxLength(4)] public string TaxCode { get; set; } = default!; //Catalog 5

}