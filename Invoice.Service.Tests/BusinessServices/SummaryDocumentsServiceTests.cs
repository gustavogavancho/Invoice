using AutoFixture;
using AutoMapper;
using Invoice.Contracts.Logger;
using Invoice.Contracts.Repositories;
using Invoice.Service.Contracts.HelperServices;
using Invoice.Service.Profiles;
using Moq;

namespace Invoice.Service.Tests.BusinessServices;

public class SummaryDocumentsServiceTests
{
    private readonly Fixture _fixture;
    private readonly Mock<IRepositoryManager> _repository;
    private readonly Mock<ILoggerManager> _logger;
    private readonly Mapper _mapper;
    private readonly Mock<IDocumentGeneratorService> _documentGeneratorService;
    private readonly Mock<ISunatService> _sunatService;

    public SummaryDocumentsServiceTests()
    {
        _fixture = new Fixture();
        _repository = new Mock<IRepositoryManager>();
        _logger = new Mock<ILoggerManager>();
        var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        _mapper = new Mapper(mapperConfiguration);
        _documentGeneratorService = new Mock<IDocumentGeneratorService>();
        _sunatService = new Mock<ISunatService>();
    }


}