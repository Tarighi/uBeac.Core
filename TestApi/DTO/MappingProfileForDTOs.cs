using AutoMapper;
using TestApi.DTO;
using TestApi.Models;

namespace TestApi
{
    public class MappingProfileForDTOs : Profile
    {
        public MappingProfileForDTOs()
        {

            CreateMap<AppUser, UserResponse>();

            CreateMap<RoleAddRequest, AppRole>();
            CreateMap<RoleUpdateRequest, AppRole>();
            CreateMap<AppRole, RoleResponse>();

            // custom entity
            CreateMap<UnitAddRequestDTO, Unit>();
            CreateMap<UnitUpdateRequestDTO, Unit>();
        }
    }
}
