using System.ComponentModel.DataAnnotations;

namespace Invoice.Entities.Models;

public class InvoicePaymentTerms
{
    public Guid Id { get; set; }
    [Required] public string PaymentId { get; set; } = "FormaPago";
    public string PaymentType { get; set; } = default!; //PaymentTerms SUNAT
    [Range(0, 9999999999999999.99)] public decimal Amount { get; set; }
    [Required] public DateTime DueDate { get; set; }
    public Guid InvoiceId { get; set; }
}