using Invoice.Shared.Request;

namespace Invoice.Service.Contracts;

public interface ISenderService
{
    Task CreateSender(SenderDataRequest senderRequest);
}