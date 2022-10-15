using System.ComponentModel.DataAnnotations;

namespace Invoice.Shared.Request;

public record InvoiceRequest
{
    [Required]
    public UblSchemeRequest UblSchemeRequest { get; set; } = default!;
}