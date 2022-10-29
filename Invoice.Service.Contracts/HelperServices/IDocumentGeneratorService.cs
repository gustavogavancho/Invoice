using Invoice.Entities.Models;
using Invoice.Shared.Request;
using UBLSunatPE;

namespace Invoice.Service.Contracts.HelperServices;

public interface IDocumentGeneratorService
{
    InvoiceType GenerateInvoiceType(InvoiceRequest request, Issuer issuer);
    DebitNoteType GenerateDebitNoteType(DebitNoteRequest request, Issuer issuer);
    CreditNoteType GenerateCreditNoteType(CreditNoteRequest request, Issuer issuer);
    SummaryDocumentsType GenerateSummaryDocumentsType(SummaryDocumentsRequest request, Issuer issuer, IEnumerable<Entities.Models.Invoice> tickets);
}