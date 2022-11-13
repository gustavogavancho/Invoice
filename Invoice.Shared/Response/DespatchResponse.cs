using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Invoice.Shared.Response;

public record DespatchResponse
{
    public Guid Id { get; set; }
    [Column(TypeName = "xml")]
    public string DespatchXml { get; set; } = default!;
    public byte[] SunatResponse { get; set; } = default!;
    public bool Accepted { get; set; }
    public string Observations { get; set; } = default!;
    public Guid IssuerId { get; set; }
    public DateTime IssueDate { get; set; }
}