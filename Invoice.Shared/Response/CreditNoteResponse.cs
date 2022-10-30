﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Invoice.Shared.Response;

public class CreditNoteResponse
{
    public Guid Id { get; set; }
    [Column(TypeName = "xml")]
    public string InvoiceSendXml { get; set; } = default!;
    public byte[] SunatResponse { get; set; } = default!;
    public bool Accepted { get; set; }
    public string Observations { get; set; } = default!;
    public Guid IssuerId { get; set; }
    public DateTime IssueDate { get; set; }
    [Range(0, 9999999999999999.99)] public decimal TaxTotalAmount { get; set; }
    [Range(0, 9999999999999999.99)] public decimal TotalAmount { get; set; }
    public bool? SummaryApproved { get; set; }
    public string? SummaryObservations { get; set; }
    public bool Canceled { get; set; }
    public string? CanceledReason { get; set; }
}