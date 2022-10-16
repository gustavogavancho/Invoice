using AutoFixture;
using AutoMapper;
using Invoice.Contracts;
using Invoice.Entities;
using Invoice.Service.Profiles;
using Invoice.Shared.Request;
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


        var senderServide = new SenderService(senderRepository.Object, _mapper);

        //Act
        await senderServide.CreateSender(senderRequest);

        senderRepository.Verify(x => x.CreateSender(It.IsAny<Sender>()), Times.Once);
    }
}