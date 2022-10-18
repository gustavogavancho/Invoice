using AutoFixture;
using Invoice.Entities;
using Invoice.Repository.Tests.ClassFixture;
using Microsoft.EntityFrameworkCore;

namespace Invoice.Repository.Tests;

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
    public async Task IssuerRepository_AddIssuerTest()
    {
        //Arrange
        var issuer = _fixture.Create<Issuer>();

        //Act
        await _issuerRepository.CreateIssuer(issuer);
        var issuerSaved = await _contextFixture.Context.Issuers.LastOrDefaultAsync();

        //Assert
        Assert.NotNull(issuerSaved);
        Assert.Equal(issuer.IssuerName, issuerSaved.IssuerName);
    }

    [Fact]
    public async Task IssuerRepository_GetIssuersTest()
    {
        //Arrange

        //Act
        var sut = await _issuerRepository.GetIssuers();
        var issuers = await _contextFixture.Context.Issuers.ToListAsync();

        //Assert
        Assert.NotNull(sut);
        Assert.Equal(issuers.Count, sut.Count());
    }

    [Fact]
    public async Task IssuerRepository_GetIssuerTest()
    {
        //Arrange
        var id = Guid.Parse("CCE03168-F901-4B23-AE9C-FC031D9DC888");

        //Act
        var sut = await _issuerRepository.GetIssuer(id);

        //Assert
        Assert.NotNull(sut);
    }

    [Fact]
    public async Task IssuerRepository_UpdateIssuerTest()
    {
        //Arrange
        var issuer = _fixture.Create<Issuer>();
        var id = Guid.Parse("CCE03168-F901-4B23-AE9C-FC031D9DC888");

        //Act
        await _issuerRepository.UpdateIssuer(id, issuer);
        var issuerSaved = await _contextFixture.Context.Issuers.FindAsync(id);

        //Assert
        Assert.Equal(issuerSaved.IssuerName, issuer.IssuerName);
    }

    [Fact]
    public async Task IssuerRepository_DeleteIssuerTest()
    {
        //Arrange 
        var id = Guid.Parse("990A5761-BFEA-4572-B0FE-08DAB08EACF6");

        //Act
        await _issuerRepository.DeleteIssuer(id);
        var issuerSaved = await _contextFixture.Context.Issuers.FindAsync(id);

        //Assert
        Assert.Null(issuerSaved);
    }
}
