using Invoice.Shared.Request;
using Invoice.Shared.Response;

namespace Invoice.Service.Contracts;

public interface ISenderService
{
    Task CreateSender(SenderDataRequest senderRequest);
    Task<SenderResponse> GetSender(Guid guid);
    Task<List<SenderResponse>> GetSenders();
    Task UpdateSender(Guid id, SenderDataRequest senderDataRequestRequest);
}