using Invoice.Shared.Request;
using Invoice.Shared.Response;

namespace Invoice.Service.Contracts;

public interface ISenderService
{
    Task CreateSender(SenderDataRequest senderRequest);
    Task<List<SenderResponse>> GetSenders();
}