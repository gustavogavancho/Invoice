﻿using AutoMapper;
using Invoice.Entities.Models;
using Invoice.Shared.Request;
using Invoice.Shared.Response;

namespace Invoice.Service.Profiles;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<Issuer, IssuerRequest>().ReverseMap();
		CreateMap<Issuer, IssuerResponse>().ReverseMap();

		CreateMap<Entities.Models.Invoice, InvoiceRequest>().ReverseMap();
        CreateMap<Entities.Models.Invoice, InvoiceResponse>().ReverseMap();
        CreateMap<InvoiceDetail, InvoiceDetailRequest>().ReverseMap();

        CreateMap<Entities.Models.Invoice, DebitNoteRequest>()
            .ForMember(dest => dest.DebitNoteDetail, opt => opt.MapFrom(x => x.InvoiceDetail))
            .ReverseMap();
        CreateMap<Entities.Models.Invoice, DebitNoteResponse>().ReverseMap();
        CreateMap<InvoiceDetail, DebitNoteDetailRequest>().ReverseMap();

        CreateMap<PaymentTerms, PaymentTermsRequest>().ReverseMap();
        CreateMap<ProductDetails, ProductDetailsRequest>().ReverseMap();
        CreateMap<Receiver, ReceiverRequest>().ReverseMap();
        CreateMap<TaxSubTotal, TaxSubTotalRequest>().ReverseMap();
    }
}