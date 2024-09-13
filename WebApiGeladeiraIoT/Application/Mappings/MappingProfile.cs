using ApiRefrigerator.DTOs;
using ApiRefrigerator.Models;
using AutoMapper;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateRefrigeratorItemDTO, Refrigerator>();
            CreateMap<UpdateRefrigeratorItemDTO, Refrigerator>();
        }
    }
}