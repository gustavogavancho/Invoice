namespace Invoice.Entities.ConfigurationModels;

public class SunatConfiguration
{
    public string UrlInvoice { get; set; } = default!;
    public string UrlDespatchAdvice { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!; 
}