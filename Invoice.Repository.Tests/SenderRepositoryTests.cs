using AutoFixture;
using Invoice.Entities;
using Invoice.Repository.Tests.ClassFixture;
using Microsoft.EntityFrameworkCore;

namespace Invoice.Repository.Tests
{
    public class SenderRepositoryTests : IClassFixture<InvoiceContextClassFixture>
    {
        private readonly InvoiceContextClassFixture _contextFixture;
        private readonly SenderRepository _senderRepository;

        public SenderRepositoryTests(InvoiceContextClassFixture contextFixture)
        {
            _contextFixture = contextFixture;
            _senderRepository = new SenderRepository(_contextFixture.Context);
        }

        [Fact]
        public async Task AddSenderTest()
        {
            //Arrange
            var fixture = new Fixture();
            var sender = fixture.Create<Sender>();

            //Act
            await _senderRepository.CreateSender(sender);
            var senderSaved = await _contextFixture.Context.Senders.LastOrDefaultAsync();


            //Assert
            Assert.NotNull(senderSaved);
            Assert.Equal(sender.SenderName, senderSaved.SenderName);
        }

        [Fact]
        public async Task GetSendersTest()
        {
            //Arrange

            //Act
            var sut = await _senderRepository.GetSenders();
            var senders = await _contextFixture.Context.Senders.ToListAsync();

            //Assert
            Assert.Equal(senders.Count, sut.Count());
        }

        [Fact]
        public async Task GetSenderTest()
        {
            //Arrange
            var id = Guid.Parse("CCE03168-F901-4B23-AE9C-FC031D9DC888");

            //Act
            Sender sut = await _senderRepository.GetSender(id);

            //Assert
            Assert.NotNull(sut);
        }

        [Fact]
        public async Task UpdateSenderTest()
        {
            //Arrange
            var fixture = new Fixture();
            var sender =  fixture.Create<Sender>();
            var id = Guid.Parse("CCE03168-F901-4B23-AE9C-FC031D9DC888");

            //Act
            await _senderRepository.UpdateSender(id, sender);
            var senderSaved = await _contextFixture.Context.Senders.FindAsync(id);

            var check = await _contextFixture.Context.Senders.ToListAsync();

            //Assert
            Assert.Equal(senderSaved.SenderName, sender.SenderName);
        }
    }
}