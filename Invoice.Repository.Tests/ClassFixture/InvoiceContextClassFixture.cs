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

        SeedSender();
    }

    private void SeedSender()
    {
        Context.Senders.Add(new Sender
        {
            Id = Guid.Parse("CCE03168-F901-4B23-AE9C-FC031D9DC888"),
            SenderId = 20606022779,
            SenderName = "SWIFTLINE SAC",
            SenderType = "6",
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