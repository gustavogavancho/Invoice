using AutoMapper;
using Invoice.Contracts.Logger;
using Invoice.Contracts.Repositories;
using Invoice.Entities.Exceptions;
using Invoice.Entities.Models;
using Invoice.Service.Contracts.BusinessServices;
using Invoice.Shared.Request;
using Invoice.Shared.Response;

namespace Invoice.Service.BusinessServices;

public class IssuerService : IIssuerService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public IssuerService(IRepositoryManager repository, 
        ILoggerManager logger,
        IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IssuerResponse> CreateIssuerAsync(IssuerRequest issuerRequest)
    {
        var issuer = _mapper.Map<IssuerRequest, Issuer>(issuerRequest);

        _repository.Issuer.CreateIssuer(issuer);
        await _repository.SaveAsync();

        var issuerResponse = _mapper.Map<Issuer, IssuerResponse>(issuer);

        return issuerResponse;
    }

    public async Task DeleteIssuerAsync(Guid id, bool trackChanges)
    {
        var issuer = await GetIssuerAndCheckIfItExists(id, trackChanges);

        _repository.Issuer.DeleteIssuer(issuer);
        await _repository.SaveAsync();
    }

    public async Task<IssuerResponse> GetIssuerAsync(Guid id, bool trackChanges)
    {
        var issuer = await GetIssuerAndCheckIfItExists(id, trackChanges);

        var issuerResponse = _mapper.Map<Issuer, IssuerResponse>(issuer);
        return issuerResponse;
    }

    public async Task<IEnumerable<IssuerResponse>> GetIssuersAsync(bool trackChanges)
    {
        var issuers = await _repository.Issuer.GetIssuersAsync(trackChanges);

        var issuerResponses = _mapper.Map<IEnumerable<Issuer>, IEnumerable<IssuerResponse>>(issuers);

        return issuerResponses;
    }

    public async Task UpdateIssuerAsync(Guid id, IssuerRequest issuerRequest, bool trackChanges)
    {
        var issuer = await GetIssuerAndCheckIfItExists(id, trackChanges);

        _mapper.Map(issuer, issuerRequest);
        await _repository.SaveAsync();
    }

    private async Task<Issuer> GetIssuerAndCheckIfItExists(Guid id, bool trackChanges)
    {
        var issuer = await _repository.Issuer.GetIssuerAsync(id, trackChanges);

        if (issuer is null)
            throw new IssuerNotFoundException(id);

        return issuer;
    }
}