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
            CreateMap<ManufacturerAddRequestDTO, Manufacturer>();
            CreateMap<ManufacturerUpdateRequestDTO, Manufacturer>();
            CreateMap<Manufacturer, ManufacturerViewDTO>();

            CreateMap<ProductAddRequestDTO, Product>();
            CreateMap<ProductUpdateRequestDTO, Product>();
            CreateMap<Product, ProductViewDTO>();

            CreateMap<FirmwareAddRequestDTO, Firmware>();
            CreateMap<FirmwareUpdateRequestDTO, Firmware>();
            CreateMap<Firmware, FirmwareViewDTO>();

            CreateMap<User, UserResponse>();
            CreateMap<RegisterRequest, User>();
            CreateMap<AuthResult<Guid, User>, LoginResponse>();

            CreateMap<RoleAddRequest, Role>();
            CreateMap<RoleUpdateRequest, Role>();
            CreateMap<Role, RoleResponse>();
        }
    }
}
