using AutoFixture;
using AutoMapper;
using Invoice.Contracts.Logger;
using Invoice.Contracts.Repositories;
using Invoice.Entities.Models;
using Invoice.Service.BusinessServices;
using Invoice.Service.Profiles;
using Invoice.Shared.Request;
using Moq;

namespace Invoice.Service.Tests.BusinessServices;

public class IssuerServiceTests
{
    private readonly Mapper _mapper;
    private readonly Mock<ILoggerManager> _logger;
    private readonly Fixture _fixture;
    private readonly Mock<IRepositoryManager> _repository;

    public IssuerServiceTests()
    {
        var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        _mapper = new Mapper(mapperConfiguration);
        _logger = new Mock<ILoggerManager>();
        _fixture = new Fixture();
        _repository = new Mock<IRepositoryManager>();
    }

    [Fact]
    public async Task IssuerService_CreateIssuerAsyncTest()
    {
        //Arrange
        var issuerRequest = _fixture.Create<IssuerRequest>();
        var issuer = _mapper.Map<IssuerRequest, Issuer>(issuerRequest);
        _repository.Setup(x => x.Issuer.CreateIssuer(issuer)).Verifiable();

        //Act
        var issuerService = new IssuerService(_repository.Object, _logger.Object, _mapper);
        var sut = await issuerService.CreateIssuerAsync(issuerRequest);

        //Assert
        _repository.Verify(x => x.SaveAsync(), Times.Once);
        Assert.NotNull(sut);
        Assert.Equal(sut.IssuerName, issuerRequest.IssuerName);
    }

    [Fact]
    public async Task IssuerService_GetIssuersAsyncTest()
    {
        //Arrange
        var issuers = _fixture.Create<List<Issuer>>();
        _repository.Setup(x => x.Issuer.GetIssuersAsync(false)).ReturnsAsync(issuers);

        //Act
        var issuerService = new IssuerService(_repository.Object, _logger.Object, _mapper);
        var sut = await issuerService.GetIssuersAsync(false);

        //Assert
        Assert.NotNull(sut);
        Assert.True(sut.Count() > 1, "Expected sut to be greater than 1");
    }

    [Fact]
    public async Task IssuerService_GetIssuerAsyncTest()
    {
        //Arrange
        var issuer = _fixture.Create<Issuer>();
        var id = Guid.Parse("CCE03168-F901-4B23-AE9C-FC031D9DC888");
        _repository.Setup(x => x.Issuer.GetIssuerAsync(id, false)).ReturnsAsync(issuer);

        //Act
        var issuerService = new IssuerService(_repository.Object, _logger.Object, _mapper);
        var sut = await issuerService.GetIssuerAsync(id, false);

        //Assert
        Assert.NotNull(sut);
    }

    [Fact]
    public async Task IssuerService_UpdateIssuerAsyncTest()
    {
        //Arrange
        var issuerRequest = _fixture.Create<IssuerRequest>();
        var issuer = _mapper.Map<IssuerRequest, Issuer>(issuerRequest);
        var id = Guid.Parse("CCE03168-F901-4B23-AE9C-FC031D9DC888");
        _repository.Setup(x => x.Issuer.GetIssuerAsync(id, true)).ReturnsAsync(issuer);

        //Act
        var issuerService = new IssuerService(_repository.Object, _logger.Object, _mapper);
        await issuerService.UpdateIssuerAsync(id, issuerRequest, true);

        //Assert
        _repository.Verify(x => x.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task IssuerService_DeleteIssuerAsyncTest()
    {
        //Arrange 
        var id = Guid.Parse("CCE03168-F901-4B23-AE9C-FC031D9DC888");
        var issuer = _fixture.Create<Issuer>();
        _repository.Setup(x => x.Issuer.GetIssuerAsync(id, false)).ReturnsAsync(issuer);

        //Act
        var issuerService = new IssuerService(_repository.Object, _logger.Object, _mapper);
        await issuerService.DeleteIssuerAsync(id, false);

        //Assert
        _repository.Verify(x => x.Issuer.DeleteIssuer(issuer), Times.Once);
        _repository.Verify(x => x.SaveAsync(), Times.Once);
    }
}