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

            //CreateMap<IdentityResult, RegisterResponse>()
            //    .ForMember(x => x.Errors, x => x.MapFrom(y => y.Errors));


            // custom entity
            CreateMap<UnitAddRequestDTO, Unit>();
            CreateMap<UnitUpdateRequestDTO, Unit>();
        }
    }
}
