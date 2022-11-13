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

        CreateMap<Despatch, DespatchRequest>().ReverseMap();
        CreateMap<Despatch, DespatchResponse>().ReverseMap();
        CreateMap<DespatchDetail, DespatchDetailRequest>().ReverseMap();
        CreateMap<DespatchDeliveryCustomer, DespatchPartyRequest>().ReverseMap();
        CreateMap<DespatchSellerSupplier, DespatchPartyRequest>().ReverseMap();
        CreateMap<DespatchShipment, DespatchShipmentRequest>().ReverseMap();
        CreateMap<DespatchProductDetails, DespatchProductDetailsRequest>().ReverseMap();

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

        CreateMap<InvoicePaymentTerms, InvoicePaymentTermsRequest>().ReverseMap();
        CreateMap<InvoiceProductDetails, InvoiceProductDetailsRequest>().ReverseMap();
        CreateMap<InvoiceReceiver, ReceiverRequest>().ReverseMap();
        CreateMap<InvoiceTaxSubTotal, InvoiceTaxSubTotalRequest>().ReverseMap();
    }
}