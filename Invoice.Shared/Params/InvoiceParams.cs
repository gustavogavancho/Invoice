using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Invoice.Shared.Params;

public record InvoiceParams
{
    [BindRequired] public string Serie { get; set; } = default!;
    [BindRequired] public uint SerialNumber { get; set; }
    [BindRequired] public uint CorrelativeNumber { get; set; }
}