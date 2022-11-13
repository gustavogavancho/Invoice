using AutoFixture;
using Invoice.Contracts.Repositories;
using Invoice.Entities.Models;
using Invoice.Repository.Repositories;
using Invoice.Repository.Tests.ClassFixture;
using Microsoft.EntityFrameworkCore;

namespace Invoice.Repository.Tests.Repositories;

public class DespatchRepositoryTests : IClassFixture<InvoiceContextClassFixture>
{
    private readonly InvoiceContextClassFixture _contextFixture;
    private readonly DespatchRepository _despatchRepository;
    private readonly Fixture _fixture;

    public DespatchRepositoryTests(InvoiceContextClassFixture contextFixture)
    {
        _contextFixture = contextFixture;
        _despatchRepository = new DespatchRepository(_contextFixture.Context);
        _fixture = new Fixture();
    }

    [Fact]
    public async Task DespatchRepository_CreateDespatchTest()
    {
        //Arrange
        var despatch = _fixture.Create<Despatch>();
        despatch.ProductsDetails = new List<DespatchProductDetails>();

        //Act
        _despatchRepository.CreateDespatch(despatch);
        await _contextFixture.Context.SaveChangesAsync();

        var despatchSaved = await _contextFixture.Context.Despatch.LastOrDefaultAsync();

        //Assert
        Assert.NotNull(despatchSaved);
        Assert.Equal(despatch.DespatchXml, despatchSaved.DespatchXml);
    }

    [Fact]
    public async Task DespatchRepository_GetDespatchesAsyncTest()
    {
        //Arrange


        //Act
        var sut = await _despatchRepository.GetDespatchesAsync(false);

        //Assert
        Assert.NotNull(sut);
        Assert.True(sut.Count() > 1, "Expected sut to be greater than 1");
    }

    [Fact]
    public async Task DespatchRepository_GetDespatchAsyncTest()
    {
        //Arrange
        var id = Guid.Parse("8F19F007-D7B5-4262-B279-AF39D68D06D3");

        //Act
        var sut = await _despatchRepository.GetDespatchAsync(id, false);

        //Assert
        Assert.NotNull(sut);
    }

    [Fact]
    public async Task DespatchRepository_UpdateTest()
    {
        //Arrange
        var issuer = _fixture.Create<Despatch>();
        var id = Guid.Parse("8F19F007-D7B5-4262-B279-AF39D68D06D3");

        //Act
        var despatchToUpdate = await _despatchRepository.GetDespatchAsync(id, true);
        despatchToUpdate.DespatchXml = issuer.DespatchXml;
        _despatchRepository.Update(despatchToUpdate);
        await _contextFixture.Context.SaveChangesAsync();

        var despatchSaved = await _contextFixture.Context.Despatch.FindAsync(id);

        //Assert
        Assert.NotNull(despatchSaved);
        Assert.Equal(despatchSaved.DespatchXml, issuer.DespatchXml);
    }

    [Fact]
    public async Task DespatchRepository_DeleteDespatchTest()
    {
        //Arrange 
        var id = Guid.Parse("C27A64AC-CBD3-45D0-9A1C-01B0D4AB1599");
        var despatchToDelete = await _despatchRepository.GetDespatchAsync(id, true);

        //Act
        _despatchRepository.DeleteDespatch(despatchToDelete);
        await _contextFixture.Context.SaveChangesAsync();
        var despatchSaved = await _contextFixture.Context.Despatch.FindAsync(id);

        //Assert
        Assert.Null(despatchSaved);
    }
}