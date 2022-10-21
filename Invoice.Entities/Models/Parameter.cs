namespace Invoice.Entities.Models;

public class Parameter
{
    public Guid Id { get; set; }
    public string Group { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Value { get; set; } = default!; 
}