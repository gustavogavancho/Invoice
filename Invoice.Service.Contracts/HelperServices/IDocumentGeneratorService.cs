using Invoice.Entities.Models;
using Invoice.Shared.Request;
using UBLSunatPE;

namespace Invoice.Service.Contracts.HelperServices;

public interface IDocumentGeneratorService
{
    InvoiceType GenerateInvoiceType(InvoiceRequest request, Issuer issuer);
    CreditNoteType GenerateCreditNoteType(NoteRequest request, Issuer issuer);
    DebitNoteType GenerateDebitNoteType(NoteRequest request, Issuer issuer);
    DespatchAdviceType GenerateDespatchAdviceType(DespatchRequest request, Issuer issuer);
    SummaryDocumentsType GenerateSummaryDocumentsType(SummaryDocumentsRequest request, Issuer issuer, IEnumerable<Entities.Models.Invoice> tickets);
    VoidedDocumentsType GenerateVoidedDocumentsType(VoidedDocumentsRequest request, Issuer issuer);
}