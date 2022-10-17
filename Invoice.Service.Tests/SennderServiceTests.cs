using AutoFixture;
using AutoMapper;
using Invoice.Contracts;
using Invoice.Entities;
using Invoice.Service.Profiles;
using Invoice.Shared.Request;
using Invoice.Shared.Response;
using Moq;

namespace Invoice.Service.Tests;

public class SennderServiceTests
{
    private readonly Mapper _mapper;

    public SennderServiceTests()
    {
        var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        _mapper = new Mapper(mapperConfiguration);
    }

    [Fact]
    public async Task CreateSenderTest()
    {
        //Arrange
        var fixture = new Fixture();
        var senderRequest = fixture.Create<SenderDataRequest>();

        var sender = _mapper.Map<SenderDataRequest, Sender>(senderRequest);

        var senderRepository = new Mock<ISenderRepository>();
        senderRepository.Setup(x => x.CreateSender(sender))
            .Callback<Sender>((senderCallBack) => sender = senderCallBack);


        var senderService = new SenderService(senderRepository.Object, _mapper);

        //Act
        await senderService.CreateSender(senderRequest);

        //Assert
        senderRepository.Verify(x => x.CreateSender(It.IsAny<Sender>()), Times.Once);
    }

    [Fact]
    public async Task GetSendersTest()
    {
        //Arrange
        var fixture = new Fixture();
        var senders = fixture.Create<List<Sender>>();

        var senderRepository = new Mock<ISenderRepository>();
        senderRepository.Setup(x => x.GetSenders())
            .ReturnsAsync(senders);

        //Act
        var senderService = new SenderService(senderRepository.Object, _mapper);

        var sut = await senderService.GetSenders();

        //Assert
        foreach (var sender in sut)
        {
            Assert.IsType<SenderResponse>(sender);
        }

        Assert.Equal(3, sut.Count());
    }

    [Fact]
    public async Task GetSenderTest()
    {
        //Arrange
        var fixture = new Fixture();
        var sender = fixture.Create<Sender>();

        var senderRepository = new Mock<ISenderRepository>();
        senderRepository.Setup(x => x.GetSender(It.IsAny<Guid>()))
            .ReturnsAsync(sender);

        //Act
        var senderService = new SenderService(senderRepository.Object, _mapper);

        SenderResponse sut = await senderService.GetSender(It.IsAny<Guid>());

        //Assert
        Assert.NotNull(sut);
        senderRepository.Verify(x => x.GetSender(It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public async Task UpdateSenderTest()
    {
        //Arrange
        var fixture = new Fixture();
        var senderDataRequestToUpdate = fixture.Create<SenderDataRequest>();
        var id = Guid.Parse("CCE03168-F901-4B23-AE9C-FC031D9DC888");

        var senderRepository = new Mock<ISenderRepository>();
        senderRepository.Setup(x => x.UpdateSender(It.IsAny<Guid>(), It.IsAny<Sender>())).Verifiable();

        var senderService = new SenderService(senderRepository.Object, _mapper);


        //Act
        await senderService.UpdateSender(id, senderDataRequestToUpdate);

        //Assert
        senderRepository.Verify(x => x.UpdateSender(It.IsAny<Guid>(), It.IsAny<Sender>()), Times.Once);
    }
}