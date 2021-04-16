using AutoMapper;
using TestApi.DTO;
using TestApi.Models;

namespace TestApi
{
    public class MappingProfileForDTOs : Profile
    {
        public MappingProfileForDTOs()
        {
            CreateMap<UnitAddRequestDTO, Unit>();
            CreateMap<UnitUpdateRequestDTO, Unit>();
        }
    }
}
