using Invoice.Contracts;
using Invoice.Entities;

namespace Invoice.Repository;

public class SenderRepository : ISenderRepository
{
    private readonly InvoiceContext _context;

    public SenderRepository(InvoiceContext context)
    {
        _context = context;
    }

    public async Task CreateSender(Sender sender)
    {
        _context.Senders.Add(sender);
        await _context.SaveChangesAsync();
    }
}