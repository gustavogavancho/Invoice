using System.ComponentModel.DataAnnotations;

namespace Invoice.Shared.Request;

public class DeliveryCustomerRequest
{
    public ulong DeliveryCustomerId { get; set; } //RUC 
    [Required] public string DeliveryCustomerName { get; set; } = default!;
    [Required, MinLength(1), MaxLength(1)] public string DeliveryCustomerType { get; set; } = default!; //Catalog 6
}
