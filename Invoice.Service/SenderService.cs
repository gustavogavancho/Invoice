﻿using AutoMapper;
using Invoice.Contracts;
using Invoice.Entities;
using Invoice.Service.Contracts;
using Invoice.Shared.Request;
using Invoice.Shared.Response;

namespace Invoice.Service;

public class SenderService : ISenderService
{
    private readonly ISenderRepository _senderRepository;
    private readonly IMapper _mapper;

    public SenderService(ISenderRepository senderRepository,
        IMapper mapper)
    {
        _senderRepository = senderRepository;
        _mapper = mapper;
    }

    public async Task CreateSender(SenderDataRequest senderRequest)
    {
        var sender = _mapper.Map<SenderDataRequest, Sender>(senderRequest);

        await _senderRepository.CreateSender(sender);
    }

    public async Task<SenderResponse> GetSender(Guid guid)
    {
        var sender = await _senderRepository.GetSender(guid);

        var senderResponse = _mapper.Map<Sender, SenderResponse>(sender);

        return senderResponse;
    }

    public async Task<List<SenderResponse>> GetSenders()
    {
        var senders = await _senderRepository.GetSenders();

        var senderResponses = _mapper.Map<List<Sender>, List<SenderResponse>>(senders);

        return senderResponses;
    }

    public async Task UpdateSender(Guid id, SenderDataRequest senderDataRequestRequest)
    {
        var sender = _mapper.Map<SenderDataRequest, Sender>(senderDataRequestRequest);

        await _senderRepository.UpdateSender(id, sender);
    }
}