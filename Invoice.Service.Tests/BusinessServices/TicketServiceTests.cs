﻿using AutoFixture;
using AutoMapper;
using Invoice.Contracts.Logger;
using Invoice.Contracts.Repositories;
using Invoice.Entities.Models;
using Invoice.Service.BusinessServices;
using Invoice.Service.Contracts.HelperServices;
using Invoice.Service.Profiles;
using Moq;

namespace Invoice.Service.Tests.BusinessServices;

public class TicketServiceTests
{
    private readonly Fixture _fixture;
    private readonly Mock<IRepositoryManager> _repository;
    private readonly Mock<ILoggerManager> _logger;
    private readonly Mapper _mapper;
    private readonly Mock<IDocumentGeneratorService> _documentGeneratorService;
    private readonly Mock<ISunatService> _sunatService;

    public TicketServiceTests()
    {
        _fixture = new Fixture();
        _repository = new Mock<IRepositoryManager>();
        _logger = new Mock<ILoggerManager>();
        var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        _mapper = new Mapper(mapperConfiguration);
        _documentGeneratorService = new Mock<IDocumentGeneratorService>();
        _sunatService = new Mock<ISunatService>();
    }

    [Fact]
    public async Task TicketService_GetTicketAsyncTest()
    {
        //Arrange
        var ticket = _fixture.Create<Ticket>();
        var ticketNumber = "1234567";
        _repository.Setup(x => x.Ticket.GetTicketAsync(ticketNumber, false)).ReturnsAsync(ticket);

        //Act
        var ticketService = new TicketService(_repository.Object, _logger.Object, _mapper, _documentGeneratorService.Object, _sunatService.Object);
        var sut = await ticketService.GetTicketAsync(ticketNumber, false);

        //Assert
        Assert.NotNull(sut);
    }

    [Fact]
    public async Task TicketService_GetTicketStatusAsyncTest()
    {
        //Arrange
        var ticket = _fixture.Create<Ticket>();
        var statusResponse = new Tuple<string, byte[]>("0", new byte[3]);
        var readResponse = _fixture.Create<List<string>>();

        var invoices = _fixture.Create<IEnumerable<Entities.Models.Invoice>>();
        var ticketNumber = "1234567";
        _repository.Setup(x => x.Ticket.GetTicketAsync(ticketNumber, false)).ReturnsAsync(ticket);
        _repository.Setup(x => x.Invoice.GetTicketsByIssueDateAsync(It.IsAny<DateTime>(), false, true)).ReturnsAsync(invoices);
        _sunatService.Setup(x => x.GetStatus(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(statusResponse);
        _sunatService.Setup(x => x.ReadResponse(It.IsAny<byte[]>())).Returns(readResponse);

        //Act
        var ticketService = new TicketService(_repository.Object, _logger.Object, _mapper, _documentGeneratorService.Object, _sunatService.Object);
        var sut = await ticketService.GetTicketStatusAsync(ticketNumber, false);

        //Assert
        Assert.NotNull(sut);
    }
}