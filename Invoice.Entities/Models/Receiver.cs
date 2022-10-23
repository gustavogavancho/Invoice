using System.ComponentModel.DataAnnotations;

namespace Invoice.Entities.Models;

public class Receiver
{
    public Guid Id { get; set; }
    [Range(10000000000, 99999999999)] public ulong ReceiverId { get; set; } //RUC 
    public string ReceiverName { get; set; } = default!;
    [MinLength(1), MaxLength(1)] public string ReceiverType { get; set; } = default!; //Catalog 6
    public string FullAddress { get; set; } = default!;
    public Guid InvoiceId { get; set; }
}