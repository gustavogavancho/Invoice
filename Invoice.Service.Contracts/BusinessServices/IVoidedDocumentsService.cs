using Invoice.Shared.Request;
using Invoice.Shared.Response;

namespace Invoice.Service.Contracts.BusinessServices;

public interface IVoidedDocumentsService
{
    Task<VoidedDocumentsResponse> CreateVoidedDocumentsAsync(Guid id, VoidedDocumentsRequest request, bool trackChanges);
}