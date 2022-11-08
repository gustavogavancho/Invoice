using AutoMapper;
using Invoice.Entities.Models;
using Invoice.Shared.Request;
using Invoice.Shared.Response;

namespace Invoice.Service.Profiles;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
        CreateMap<Ticket, TicketResponse>().ReverseMap();

		CreateMap<Issuer, IssuerRequest>().ReverseMap();
		CreateMap<Issuer, IssuerResponse>().ReverseMap();

        CreateMap<Entities.Models.Invoice, DespatchAdviceRequest>().ReverseMap();

        CreateMap<Entities.Models.Invoice, InvoiceRequest>().ReverseMap();
        CreateMap<Entities.Models.Invoice, InvoiceResponse>().ReverseMap();
        CreateMap<InvoiceDetail, InvoiceDetailRequest>().ReverseMap();

        CreateMap<Entities.Models.Invoice, NoteRequest>()
            .ForMember(dest => dest.NoteDetail, src => src.MapFrom(x => x.InvoiceDetail))
            .ReverseMap();

        CreateMap<Entities.Models.Invoice, NoteRequest>()
            .ForMember(dest => dest.NoteDetail, src => src.MapFrom(x => x.InvoiceDetail))
            .ReverseMap();

        CreateMap<InvoiceDetail, NoteDetailRequest>().ReverseMap();

        CreateMap<PaymentTerms, InvoicePaymentTermsRequest>().ReverseMap();
        CreateMap<ProductDetails, InvoiceProductDetailsRequest>().ReverseMap();
        CreateMap<Receiver, ReceiverRequest>().ReverseMap();
        CreateMap<TaxSubTotal, InvoiceTaxSubTotalRequest>().ReverseMap();
    }
}