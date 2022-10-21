using AutoFixture;
using Invoice.Repository.Repositories;
using Invoice.Repository.Tests.ClassFixture;
using Microsoft.EntityFrameworkCore;

namespace Invoice.Repository.Tests.Repositories;

public class InvoiceRepositoryTests : IClassFixture<InvoiceContextClassFixture>
{
    private readonly InvoiceContextClassFixture _contextFixture;
    private readonly InvoiceRepository _invoiceRepository;
    private readonly Fixture _fixture;

    public InvoiceRepositoryTests(InvoiceContextClassFixture contextFixture)
    {
        _contextFixture = contextFixture;
        _invoiceRepository = new InvoiceRepository(_contextFixture.Context);
        _fixture = new Fixture();
    }

    [Fact]
    public async Task InvoiceRepository_CreateInvoiceTest()
    {
        //Arrange
        var invoice = _fixture.Create<Entities.Models.Invoice>();

        //Act
        _invoiceRepository.CreateInvoice(invoice);
        await _contextFixture.Context.SaveChangesAsync();

        var invoiceSaved = await _contextFixture.Context.Invoices.LastOrDefaultAsync();

        //Assert
        Assert.NotNull(invoiceSaved);
        Assert.Equal(invoice.InvoiceXml, invoiceSaved.InvoiceXml);
    }

    [Fact]
    public async Task InvoiceRepository_GetInvoicesAsyncTest()
    {
        //Arrange


        //Act
        var sut = await _invoiceRepository.GetInvoicesAsync(false);

        //Assert
        Assert.NotNull(sut);
        Assert.True(sut.Count() > 1, "Expected sut to be greater than 1");
    }

    [Fact]
    public async Task InvoiceRepository_GetInvoiceAsyncTest()
    {
        //Arrange
        var id = Guid.Parse("ECE849FE-A441-4DEC-A452-A6723A38C9D0");

        //Act
        var sut = await _invoiceRepository.GetInvoiceAsync(id, false);

        //Assert
        Assert.NotNull(sut);
    }

    [Fact]
    public async Task InvoiceRepository_UpdateTest()
    {
        //Arrange
        var issuer = _fixture.Create<Entities.Models.Invoice>();
        var id = Guid.Parse("ECE849FE-A441-4DEC-A452-A6723A38C9D0");

        //Act
        var invoiceToUpdate = await _invoiceRepository.GetInvoiceAsync(id, true);
        invoiceToUpdate.InvoiceXml = issuer.InvoiceXml;
        _invoiceRepository.Update(invoiceToUpdate);
        await _contextFixture.Context.SaveChangesAsync();
        var invoiceSaved = await _contextFixture.Context.Invoices.FindAsync(id);

        //Assert
        Assert.NotNull(invoiceSaved);
        Assert.Equal(invoiceSaved.InvoiceXml, issuer.InvoiceXml);
    }

    [Fact]
    public async Task IssuerRepository_DeleteIssuerTest()
    {
        //Arrange 
        var id = Guid.Parse("2467C21B-3ACF-45CD-8A3B-A898CE07B7FC");
        var invoiceToDelete = await _invoiceRepository.GetInvoiceAsync(id, true);

        //Act
        _invoiceRepository.DeleteInvoice(invoiceToDelete);
        await _contextFixture.Context.SaveChangesAsync();
        var invoiceSaved = await _contextFixture.Context.Issuers.FindAsync(id);

        //Assert
        Assert.Null(invoiceSaved);
    }
}