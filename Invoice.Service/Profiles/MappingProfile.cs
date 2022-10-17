using AutoMapper;
using Invoice.Entities;
using Invoice.Shared.Request;
using Invoice.Shared.Response;

namespace Invoice.Service.Profiles;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<Sender, SenderDataRequest>().ReverseMap();
		CreateMap<Sender, SenderResponse>().ReverseMap();
	}
}