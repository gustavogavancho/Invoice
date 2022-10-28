using Invoice.Shared.Request;
using Invoice.Shared.Response;

namespace Invoice.Service.Contracts.BusinessServices;

public interface ISummaryDocumentsService
{
    Task<SummaryDocumentsResponse> CreateSummaryDocumentsAsync(Guid id, SummaryDocumentsRequest request, bool trackChanges);
    Task<SummaryDocumentsResponse> GetSummaryDocumentsAsync(Guid id, bool trackChanges);
}