using Invoice.Entities.Models;
using Invoice.Shared.Request;
using UBLSunatPE;

namespace Invoice.Service.Contracts.HelperServices;

public interface IDocumentGeneratorService
{
    InvoiceType GenerateInvoiceType(InvoiceRequest request, Issuer issuer);
}