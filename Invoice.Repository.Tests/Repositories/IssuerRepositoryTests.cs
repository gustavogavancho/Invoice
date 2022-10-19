using AutoFixture;
using Invoice.Entities.Models;
using Invoice.Repository.Repositories;
using Invoice.Repository.Tests.ClassFixture;
using Microsoft.EntityFrameworkCore;

namespace Invoice.Repository.Tests.Repositories;

public class IssuerRepositoryTests : IClassFixture<InvoiceContextClassFixture>
{
    private readonly InvoiceContextClassFixture _contextFixture;
    private readonly IssuerRepository _issuerRepository;
    private readonly Fixture _fixture;

    public IssuerRepositoryTests(InvoiceContextClassFixture contextFixture)
    {
        _contextFixture = contextFixture;
        _issuerRepository = new IssuerRepository(_contextFixture.Context);
        _fixture = new Fixture();
    }

    [Fact]
    public async Task IssuerRepository_CreateIssuerTest()
    {
        //Arrange
        var issuer = _fixture.Create<Issuer>();

        //Act
        _issuerRepository.CreateIssuer(issuer);
        await _contextFixture.Context.SaveChangesAsync();

        var issuerSaved = await _contextFixture.Context.Issuers.LastOrDefaultAsync();

        //Assert
        Assert.NotNull(issuerSaved);
        Assert.Equal(issuer.IssuerName, issuerSaved.IssuerName);
    }

    [Fact]
    public async Task IssuerRepository_GetIssuersAsyncTest()
    {
        //Arrange
        

        //Act
        var sut = await _issuerRepository.GetIssuersAsync(false);

        //Assert
        Assert.NotNull(sut);
        Assert.True(sut.Count() > 1, "Expected sut to be greater than 1");
    }

    [Fact]
    public async Task IssuerRepository_GetIssuerAsyncTest()
    {
        //Arrange
        var id = Guid.Parse("CCE03168-F901-4B23-AE9C-FC031D9DC888");

        //Act
        var sut = await _issuerRepository.GetIssuerAsync(id, false);

        //Assert
        Assert.NotNull(sut);
    }

    [Fact]
    public async Task IssuerRepository_UpdateTest()
    {
        //Arrange
        var issuer = _fixture.Create<Issuer>();
        var id = Guid.Parse("CCE03168-F901-4B23-AE9C-FC031D9DC888");

        //Act
        var issuerToUpdate = await _issuerRepository.GetIssuerAsync(id, true);
        issuerToUpdate.IssuerName = issuer.IssuerName;
        _issuerRepository.Update(issuerToUpdate);
        await _contextFixture.Context.SaveChangesAsync();
        var issuerSaved = await _contextFixture.Context.Issuers.FindAsync(id);

        //Assert
        Assert.NotNull(issuerSaved);
        Assert.Equal(issuerSaved.IssuerName, issuer.IssuerName);
    }

    [Fact]
    public async Task IssuerRepository_DeleteIssuerTest()
    {
        //Arrange 
        var id = Guid.Parse("990A5761-BFEA-4572-B0FE-08DAB08EACF6");
        var issuerToDelete = await _issuerRepository.GetIssuerAsync(id, true);

        //Act
        _issuerRepository.DeleteIssuer(issuerToDelete);
        await _contextFixture.Context.SaveChangesAsync();
        var issuerSaved = await _contextFixture.Context.Issuers.FindAsync(id);

        //Assert
        Assert.Null(issuerSaved);
    }
}
