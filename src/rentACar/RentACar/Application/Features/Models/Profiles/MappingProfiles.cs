using Application.Features.Models.Queries.GetList;
using Application.Features.Models.Queries.GetListByDynamic;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Models.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Model, GetListModelListItemDto>()
            .ForMember(destinationMember: md => md.BrandName, memberOptions: opt => opt.MapFrom(m => m.Brand.Name)) // Benim bir tane BrandName değerim var. Onu git Brand nesnesinin içinde ki Name değerinden al.
            .ForMember(destinationMember: md => md.FuelName, memberOptions: opt => opt.MapFrom(m => m.Fuel.Name))
            .ForMember(destinationMember: md => md.TransmissionName, memberOptions: opt => opt.MapFrom(m => m.Transmission.Name))
            .ReverseMap();
        // İsimler tam olarak uyuşmadığında bu yöntemi kullanmak zorundayız. BrandName - Brand.Name yapısını AutoMapper kendi bulup yapabiliyor.
        CreateMap<Model, GetListByDynamicModelListItemDto>()
            .ForMember(destinationMember: md => md.BrandName, memberOptions: opt => opt.MapFrom(m => m.Brand.Name))
            .ForMember(destinationMember: md => md.FuelName, memberOptions: opt => opt.MapFrom(m => m.Fuel.Name))
            .ForMember(destinationMember: md => md.TransmissionName, memberOptions: opt => opt.MapFrom(m => m.Transmission.Name))
            .ReverseMap();


        CreateMap<Paginate<Model>, GetListResponse<GetListModelListItemDto>>().ReverseMap();
        CreateMap<Paginate<Model>, GetListResponse<GetListByDynamicModelListItemDto>>().ReverseMap();
    }
}