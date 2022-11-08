using System.ComponentModel.DataAnnotations;

namespace Invoice.Shared.Request;

//FYI: There are three type of payment terms => Contado, Credito, Cuotas
public record InvoicePaymentTermsRequest
{
    [Required] public string PaymentId { get; set; } = "FormaPago";
    public string PaymentType { get; set; } = default!; //PaymentTerms SUNAT
    [Range(0, 9999999999999999.99)] public decimal Amount { get; set; }
    [Required] public DateTime DueDate { get; set; }
}