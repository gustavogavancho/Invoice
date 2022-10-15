using System.ComponentModel.DataAnnotations;

namespace Invoice.Shared.Request;

public record UblSchemeRequest
{
    [Required, MinLength(3), MaxLength(3)] public string UblVersionId { get; set; } = default!;
    [Required, MinLength(3), MaxLength(3)] public string CustomizationId { get; set; } = default!;
}