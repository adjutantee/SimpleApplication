using Application.DTO;
using AutoMapper;
using Domain.Enums;
using Domain.Models;

namespace Application.Mapping
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                // Resource - ResourceDto
                config.CreateMap<Resource, ResourceDto>()
                    .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State.ToString()));

                config.CreateMap<ResourceDto, Resource>()
                    .ForMember(dest => dest.State, opt => opt.MapFrom(src => Enum.Parse<State>(src.State)));

                // Unit - UnitDto
                config.CreateMap<Unit, UnitDto>()
                    .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State.ToString()));

                config.CreateMap<UnitDto, Unit>()
                    .ForMember(dest => dest.State, opt => opt.MapFrom(src => Enum.Parse<State>(src.State)));
                    
                // Receipt - ReceiptDto
                config.CreateMap<Receipt, ReceiptDto>().ReverseMap();

                // ReceiptItem - ReceiptItemDto
                config.CreateMap<ReceiptItem, ReceiptItemDto>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
