using Invoice.Shared.Request;
using Invoice.Shared.Response;

namespace Invoice.Service.Contracts.BusinessServices;

public interface ISummaryDocumentsService
{
    Task<DocumentsResponse> CreateSummaryDocumentsAsync(Guid id, SummaryDocumentsRequest request, bool trackChanges);
}