using Invoice.Entities;

namespace Invoice.Contracts;

public interface ISenderRepository
{
    Task CreateSender(Sender sender);
}