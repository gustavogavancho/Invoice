using AutoFixture;
using Invoice.Contracts.Repositories;
using Invoice.Entities.Models;
using Invoice.Repository.Repositories;
using Invoice.Repository.Tests.ClassFixture;
using Microsoft.EntityFrameworkCore;

namespace Invoice.Repository.Tests.Repositories;

public class TicketRepositoryTests : IClassFixture<InvoiceContextClassFixture>
{
    private readonly InvoiceContextClassFixture _contextFixture;
    private readonly TicketRepository _ticketRepository;
    private readonly Fixture _fixture;

    public TicketRepositoryTests(InvoiceContextClassFixture contextFixture)
    {
        _contextFixture = contextFixture;
        _ticketRepository = new TicketRepository(_contextFixture.Context);
        _fixture = new Fixture();
    }

    [Fact]
    public async Task TicketRepository_CreateTicketTest()
    {
        //Arrange
        var ticket = _fixture.Create<Ticket>();

        //Act
        _ticketRepository.CreateTicket(ticket);
        await _contextFixture.Context.SaveChangesAsync();

        var ticketSaved = await _contextFixture.Context.Tickets.LastOrDefaultAsync(x => x.TicketNumber == ticket.TicketNumber);

        //Assert
        Assert.NotNull(ticketSaved);
        Assert.Equal(ticket.TicketNumber, ticketSaved.TicketNumber);
    }

    [Fact]
    public async Task TicketRepository_GetTicketsAsyncTest()
    {
        //Arrange


        //Act
        var sut = await _ticketRepository.GetTicketsAsync(false);

        //Assert
        Assert.NotNull(sut);
        Assert.True(sut.Count() > 1, "Expected sut to be greater than 1");
    }

    [Fact]
    public async Task TicketRepository_GetTicketAsyncTest()
    {
        //Arrange
        var id = Guid.Parse("C83AD961-E0C9-47AF-A3B5-5573BE4C00FE");

        //Act
        var sut = await _ticketRepository.GetTicketAsync(id, false);

        //Assert
        Assert.NotNull(sut);
    }

    [Fact]
    public async Task TicketRepository_UpdateTest()
    {
        //Arrange
        var ticket = _fixture.Create<Ticket>();
        var id = Guid.Parse("C83AD961-E0C9-47AF-A3B5-5573BE4C00FE");

        //Act
        var ticketToUpdate = await _ticketRepository.GetTicketAsync(id, true);
        ticketToUpdate.TicketNumber = ticket.TicketNumber;
        _ticketRepository.Update(ticketToUpdate);
        await _contextFixture.Context.SaveChangesAsync();
        var ticketSaved = await _contextFixture.Context.Tickets.FindAsync(id);

        //Assert
        Assert.NotNull(ticketSaved);
        Assert.Equal(ticketSaved.TicketNumber, ticket.TicketNumber);
    }

    [Fact]
    public async Task TicketRepository_DeleteTicketTest()
    {
        //Arrange 
        var id = Guid.Parse("F14DD13B-A0DC-42D4-A560-D2AAB067C9E6");
        var ticketToDelete = await _ticketRepository.GetTicketAsync(id, true);

        //Act
        _ticketRepository.DeleteTicket(ticketToDelete);
        await _contextFixture.Context.SaveChangesAsync();
        var ticketSaved = await _contextFixture.Context.Tickets.FindAsync(id);

        //Assert
        Assert.Null(ticketSaved);
    }
}