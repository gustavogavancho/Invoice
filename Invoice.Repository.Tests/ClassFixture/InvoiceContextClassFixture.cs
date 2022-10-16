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
    }
}