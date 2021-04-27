using AutoMapper;
using System;
using uBeac.Core.Identity;
using uBeac.IoT.Api.DTO;
using uBeac.IoT.Models;

namespace uBeac.IoT.Api
{
    public class MappingProfileForDTOs : Profile
    {
        public MappingProfileForDTOs()
        {
            CreateMap<ManufacturerAddRequest, Manufacturer>();
            CreateMap<ManufacturerUpdateRequest, Manufacturer>();
            CreateMap<Manufacturer, ManufacturerResponse>();

            CreateMap<ProductAddRequest, Product>();
            CreateMap<ProductUpdateRequest, Product>();
            CreateMap<Product, ProductResponse>();

            CreateMap<FirmwareAddRequest, Firmware>();
            CreateMap<FirmwareUpdateRequest, Firmware>();
            CreateMap<Firmware, FirmwareResponse>();

            CreateMap<User, UserResponse>();
            CreateMap<RegisterRequest, User>();
            CreateMap<AuthResult<Guid, User>, LoginResponse>();

            CreateMap<RoleAddRequest, Role>();
            CreateMap<RoleUpdateRequest, Role>();
            CreateMap<Role, RoleResponse>();

            CreateMap<TeamAddRequest, Team>();
            CreateMap<TeamUpdateRequest, Team>();
            CreateMap<Team, TeamResponse>();

        }
    }
}
