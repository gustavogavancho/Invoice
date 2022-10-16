using AutoMapper;
using Invoice.Entities;
using Invoice.Shared.Request;

namespace Invoice.Service.Profiles;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<Sender, SenderDataRequest>().ReverseMap();
	}
}