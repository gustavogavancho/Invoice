using AutoMapper;
using Invoice.Contracts;
using Invoice.Entities;
using Invoice.Service.Contracts;
using Invoice.Shared.Request;

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
}