﻿using AutoFixture;
using AutoMapper;
using Invoice.Contracts.Logger;
using Invoice.Contracts.Repositories;
using Invoice.Entities.Models;
using Invoice.Service.BusinessServices;
using Invoice.Service.Contracts.HelperServices;
using Invoice.Service.Profiles;
using Invoice.Shared.Request;
using Invoice.Shared.Response;
using Moq;
using System.Xml;
using UBLSunatPE;

namespace Invoice.Service.Tests.BusinessServices;

public class DespatchAdviceServiceTests
{
    private readonly Fixture _fixture;
    private readonly Mock<IRepositoryManager> _repository;
    private readonly Mock<ILoggerManager> _logger;
    private readonly Mapper _mapper;
    private readonly Mock<IDocumentGeneratorService> _documentGeneratorService;
    private readonly Mock<ISunatService> _sunatService;

    public DespatchAdviceServiceTests()
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
    public async Task DespatchAdviceService_CreateDespaAsyncTest()
    {
        //Arrange
        var request = _fixture.Create<DespatchAdviceRequest>();
        var issuer = _fixture.Create<Issuer>();

        _repository.Setup(x => x.Issuer.GetIssuerAsync(It.IsAny<Guid>(), false)).ReturnsAsync(issuer);
        _repository.Setup(x => x.Invoice.CreateInvoice(It.IsAny<Entities.Models.Invoice>())).Verifiable();
        _sunatService.Setup(x => x.SerializeXmlDocument(typeof(InvoiceType), It.IsAny<InvoiceType>())).Returns(It.IsAny<string>());
        _sunatService.Setup(x => x.SignXml(It.IsAny<String>(), It.IsAny<Issuer>(), It.IsAny<string>())).Returns(new XmlDocument());
        _sunatService.Setup(x => x.ZipXml(It.IsAny<XmlDocument>(), It.IsAny<string>())).Returns(It.IsAny<byte[]>());
        _sunatService.Setup(x => x.SendBill(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<byte[]>())).ReturnsAsync(It.IsAny<byte[]>());
        _sunatService.Setup(x => x.ReadResponse(It.IsAny<byte[]>())).Returns(new List<string> { "La Factura numero FA01-00000001, ha sido aceptada" });

        //Act
        var despatchAdviceService = new DespatchAdviceService(_repository.Object, _logger.Object, _mapper, _documentGeneratorService.Object, _sunatService.Object);
        var sut = await despatchAdviceService.CreateDespatchAdviceAsync(It.IsAny<Guid>(), request, false);

        //Assert
        Assert.NotNull(sut);
        Assert.IsType<DespatchAdviceResponse>(sut);
    }
}