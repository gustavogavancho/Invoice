using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Invoice.Shared.Params;

public class DespatchParams
{
    [BindRequired] public string Serie { get; set; } = default!;
    [BindRequired] public int SerialNumber { get; set; }
    [BindRequired] public int CorrelativeNumber { get; set; }
}