using System.ComponentModel.DataAnnotations.Schema;

namespace Invoice.Entities.Models;

public class Invoice
{
    public Guid Id { get; set; }
    [Column(TypeName = "xml")]
    public string InvoiceXml { get; set; } = default!;
    public Issuer Issuer { get; set; } = default!;
    public Guid IssuerId { get; set; }
}