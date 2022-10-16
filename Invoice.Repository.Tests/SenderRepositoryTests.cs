using AutoFixture;
using Invoice.Entities;
using Invoice.Repository.Tests.ClassFixture;
using Microsoft.EntityFrameworkCore;

namespace Invoice.Repository.Tests
{
    public class SenderRepositoryTests : IClassFixture<InvoiceContextClassFixture>
    {
        private readonly InvoiceContextClassFixture _contextFixture;

        public SenderRepositoryTests(InvoiceContextClassFixture contextFixture)
        {
            _contextFixture = contextFixture;
        }

        [Fact]
        public async Task AddSenderTest()
        {
            //Arrange
            var fixture = new Fixture();
            var sender = fixture.Create<Sender>();
            var senderRepository = new SenderRepository(_contextFixture.Context);

            //Act
            await senderRepository.CreateSender(sender);
            var senderSaved = await _contextFixture.Context.Senders.LastOrDefaultAsync();


            //Assert
            Assert.NotNull(senderSaved);
            Assert.Equal(sender.SenderName, senderSaved.SenderName);
        }
    }
}