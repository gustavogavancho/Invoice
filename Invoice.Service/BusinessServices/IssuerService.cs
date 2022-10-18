using AutoMapper;
using Invoice.Contracts.Repositories;
using Invoice.Entities;
using Invoice.Service.Contracts.BusinessServices;
using Invoice.Shared.Request;
using Invoice.Shared.Response;

namespace Invoice.Service.BusinessServices;

public class IssuerService : IIssuerService
{
    private readonly IIssuerRepository _issuerRepository;
    private readonly IMapper _mapper;

    public IssuerService(IIssuerRepository issuerRepository,
        IMapper mapper)
    {
        _issuerRepository = issuerRepository;
        _mapper = mapper;
    }

    public async Task CreateIssuer(IssuerRequest issuerRequest)
    {
        var issuer = _mapper.Map<IssuerRequest, Issuer>(issuerRequest);

        await _issuerRepository.CreateIssuer(issuer);
    }

    public async Task DeleteIssuer(Guid id)
    {
        await _issuerRepository.DeleteIssuer(id);
    }

    public async Task<IssuerResponse> GetIssuer(Guid guid)
    {
        var issuer = await _issuerRepository.GetIssuer(guid);

        var issuerResponse = _mapper.Map<Issuer, IssuerResponse>(issuer);

        return issuerResponse;
    }

    public async Task<List<IssuerResponse>> GetIssuers()
    {
        var issuers = await _issuerRepository.GetIssuers();

        var issuerResponses = _mapper.Map<List<Issuer>, List<IssuerResponse>>(issuers);

        return issuerResponses;
    }

    public async Task UpdateIssuer(Guid id, IssuerRequest issuerRequest)
    {
        var issuer = _mapper.Map<IssuerRequest, Issuer>(issuerRequest);

        await _issuerRepository.UpdateIssuer(id, issuer);
    }
}