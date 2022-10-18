using Invoice.Entities;
using Microsoft.EntityFrameworkCore;

namespace Invoice.Repository.Tests.ClassFixture;

public class InvoiceContextClassFixture
{
    public InvoiceContext Context { get; private set; }

    public InvoiceContextClassFixture()
    {
        var options = new DbContextOptionsBuilder<InvoiceContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        Context = new InvoiceContext(options);

        SeedIssuer();
    }

    private void SeedIssuer()
    {
        Context.Issuers.Add(new Issuer
        {
            Id = Guid.Parse("CCE03168-F901-4B23-AE9C-FC031D9DC888"),
            IssuerId = 20606022779,
            IssuerName = "SWIFTLINE SAC",
            IssuerType = "6",
            GeoCode = "220901",
            EstablishmentCode = "0000",
            Department = "SAN MARTIN",
            Province = "SAN MARTIN",
            District = "TARAPOTO",
            Address = "PSJE. LIMATAMBO 121"
        });

        Context.Issuers.Add(new Issuer
        {
            Id = Guid.Parse("990A5761-BFEA-4572-B0FE-08DAB08EACF6"),
            IssuerId = 20606022779,
            IssuerName = "SWIFTLINE SAC 2",
            IssuerType = "6",
            GeoCode = "220901",
            EstablishmentCode = "0000",
            Department = "SAN MARTIN",
            Province = "SAN MARTIN",
            District = "TARAPOTO",
            Address = "PSJE. LIMATAMBO 121"
        });

        Context.SaveChanges();
    }
}