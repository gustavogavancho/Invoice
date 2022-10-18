using Invoice.Contracts;
using Invoice.Entities;
using Microsoft.EntityFrameworkCore;

namespace Invoice.Repository;

public class IssuerRepository : IIssuerRepository
{
    private readonly InvoiceContext _context;

    public IssuerRepository(InvoiceContext context)
    {
        _context = context;
    }

    public async Task CreateIssuer(Issuer issuer)
    {
        _context.Issuers.Add(issuer);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteIssuer(Guid id)
    {
        var issuer = await _context.Issuers.FindAsync(id);

        if (issuer is not null)
        {
            _context.Issuers.Remove(issuer);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Issuer> GetIssuer(Guid id)
    {
        var issuer = await _context.Issuers.FindAsync(id);
        return issuer;
    }

    public async Task<List<Issuer>> GetIssuers()
    {
        var issuers = await _context.Issuers.ToListAsync();
        return issuers;
    }

    public async Task UpdateIssuer(Guid id, Issuer issuer)
    {
        var issuerDb = await _context.Issuers.FindAsync(id);

        if (issuerDb is not null)
        {
            issuerDb.IssuerName = issuer.IssuerName;
            issuerDb.IssuerType = issuer.IssuerType;
            issuerDb.Department = issuer.Department;
            issuerDb.Province = issuer.Province;
            issuerDb.District = issuer.District;
            issuerDb.Address = issuer.Address;
            issuerDb.EstablishmentCode = issuer.EstablishmentCode;
            issuerDb.GeoCode = issuer.GeoCode;
            issuerDb.BetaCertificate = issuer.BetaCertificate;
            issuerDb.ProdCertificate = issuer.ProdCertificate;
        }

        await _context.SaveChangesAsync();
    }
}