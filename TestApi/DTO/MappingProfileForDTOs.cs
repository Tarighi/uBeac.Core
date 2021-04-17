using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TestApi.DTO;
using TestApi.Models;
using uBeac.Core.Identity;
using uBeac.Core.Web;

namespace TestApi
{
    public class MappingProfileForDTOs : Profile
    {
        public MappingProfileForDTOs()
        {
            // account dto mappings

            // register
            CreateMap<RegisterRequest, User>();
            CreateMap<User, RegisterResponse>();
            CreateMap<IdentityError, Error>();

            // authenticate
            CreateMap<User, LoginResponse>();

            //CreateMap<IdentityResult, RegisterResponse>()
            //    .ForMember(x => x.Errors, x => x.MapFrom(y => y.Errors));


            // custom entity
            CreateMap<UnitAddRequestDTO, Unit>();
            CreateMap<UnitUpdateRequestDTO, Unit>();
        }
    }
}
