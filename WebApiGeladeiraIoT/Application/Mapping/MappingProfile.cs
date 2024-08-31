using ApiGeladeira.DTOs;
using ApiGeladeira.Models;
using AutoMapper;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateGeladeiraDTO, ItemGeladeira>();
            CreateMap<UpdateGeladeiraDTO, ItemGeladeira>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}