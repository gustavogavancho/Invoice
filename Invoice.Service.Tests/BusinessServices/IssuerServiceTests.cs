using AutoFixture;
using AutoMapper;
using Invoice.Contracts.Repositories;
using Invoice.Entities;
using Invoice.Service.BusinessServices;
using Invoice.Service.Profiles;
using Invoice.Shared.Request;
using Invoice.Shared.Response;
using Moq;

namespace Invoice.Service.Tests.BusinessServices;

public class IssuerServiceTests
{
    private readonly Mapper _mapper;
    private readonly Fixture _fixture;
    private readonly Mock<IIssuerRepository> _issuerRepository;

    public IssuerServiceTests()
    {
        var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        _mapper = new Mapper(mapperConfiguration);
        _fixture = new Fixture();
        _issuerRepository = new Mock<IIssuerRepository>();
    }

    [Fact]
    public async Task IssuerService_CreateIssuerTest()
    {
        //Arrange
        var issuerRequest = _fixture.Create<IssuerRequest>();

        //Act
        var issuerService = new IssuerService(_issuerRepository.Object, _mapper);
        await issuerService.CreateIssuer(issuerRequest);

        //Assert
        _issuerRepository.Verify(x => x.CreateIssuer(It.IsAny<Issuer>()), Times.Once);
    }

    [Fact]
    public async Task IssuerService_GetIssuersTest()
    {
        //Arrange
        var issuers = _fixture.Create<List<Issuer>>();
        _issuerRepository.Setup(x => x.GetIssuers()).ReturnsAsync(issuers);

        //Act
        var issuerService = new IssuerService(_issuerRepository.Object, _mapper);
        var sut = await issuerService.GetIssuers();

        //Assert
        Assert.NotNull(sut);
        Assert.Equal(3, sut.Count);
        foreach (var issuer in sut)
        {
            Assert.IsType<IssuerResponse>(issuer);
        }
    }

    [Fact]
    public async Task IssuerService_GetIssuerTest()
    {
        //Arrange
        var issuer = _fixture.Create<Issuer>();
        _issuerRepository.Setup(x => x.GetIssuer(It.IsAny<Guid>()))
            .ReturnsAsync(issuer);

        //Act
        var issuerService = new IssuerService(_issuerRepository.Object, _mapper);
        IssuerResponse sut = await issuerService.GetIssuer(It.IsAny<Guid>());

        //Assert
        Assert.NotNull(sut);
        Assert.IsType<IssuerResponse>(sut);
    }

    [Fact]
    public async Task IssuerService_UpdateIssuerTest()
    {
        //Arrange
        var issuerRequest = _fixture.Create<IssuerRequest>();
        var id = Guid.Parse("CCE03168-F901-4B23-AE9C-FC031D9DC888");

        //Act
        var issuerService = new IssuerService(_issuerRepository.Object, _mapper);
        await issuerService.UpdateIssuer(id, issuerRequest);

        //Assert
        _issuerRepository.Verify(x => x.UpdateIssuer(It.IsAny<Guid>(), It.IsAny<Issuer>()), Times.Once);
    }

    [Fact]
    public async Task IssuerService_DeleteIssuerTest()
    {
        //Arrange 
        var id = Guid.Parse("CCE03168-F901-4B23-AE9C-FC031D9DC888");

        //Act
        var issuerService = new IssuerService(_issuerRepository.Object, _mapper);
        await issuerService.DeleteIssuer(id);

        //Assert
        _issuerRepository.Verify(x => x.DeleteIssuer(It.IsAny<Guid>()), Times.Once);
    }
}