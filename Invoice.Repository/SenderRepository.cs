using Invoice.Contracts;
using Invoice.Entities;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Sender> GetSender(Guid id)
    {
        var sender = await _context.Senders.FindAsync(id);
        return sender;
    }

    public async Task<List<Sender>> GetSenders()
    {
        var senders = await _context.Senders.ToListAsync();
        return senders;
    }
}