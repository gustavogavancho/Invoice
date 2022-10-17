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

    public async Task DeleteSender(Guid id)
    {
        var sender = await _context.Senders.FindAsync(id);

        if (sender is not null)
        {
            _context.Senders.Remove(sender);
            await _context.SaveChangesAsync();
        }
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

    public async Task UpdateSender(Guid id, Sender sender)
    {
        var senderDb = await _context.Senders.FindAsync(id);

        if (senderDb is not null)
        {
            senderDb.SenderName = sender.SenderName;
            senderDb.SenderType = sender.SenderType;
            senderDb.Department = sender.Department;
            senderDb.Province = sender.Province;
            senderDb.District = sender.District;
            senderDb.Address = sender.Address;
            senderDb.EstablishmentCode = sender.EstablishmentCode;
            senderDb.GeoCode = sender.GeoCode;
        }

        await _context.SaveChangesAsync();
    }
}