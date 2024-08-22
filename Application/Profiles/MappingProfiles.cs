using Application.Data;
using Application.Dto;
using Application.Dto.Roles;
using Application.Dto.RoleSystempermissions;
using Application.Dto.Systemmodules;
using Application.Dto.Systempermissions;
using Application.Dto.Systemsubmodules;
using Application.Dto.Users;
using AutoMapper;

namespace Application.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Systemmodule, ModifySystemmoduleDto>().ReverseMap();
            CreateMap<Systemmodule, SystemmoduleDto>().ReverseMap();
            CreateMap<Systemsubmodule,ModifySystemsubmoduleDto>().ReverseMap();
            CreateMap<Systempermission,ModifySystempermissionDto>().ReverseMap();
            CreateMap<Role,ModifyRoleDto>().ReverseMap();
            CreateMap<RoleSystempermission, ModifyRoleSystempermissionDto>().ReverseMap();
            CreateMap<ApplicationUser,ModifyUserDto>().ReverseMap();
            CreateMap<City, CityModifyDto>().ReverseMap();
            CreateMap<Gender, GenderModifyDto>().ReverseMap();
            CreateMap<MaritalStatus, MaritalStatusModifyDto>().ReverseMap();
            CreateMap<Nationality, NationalityModifyDto>().ReverseMap();
            CreateMap<Province, ProvinceModifyDto>().ReverseMap();
            CreateMap<Title, TitleModifyDto>().ReverseMap();
            CreateMap<Visitor, VisitorModifyDto>().ReverseMap();
            CreateMap<VisitorNumber, VisitorNumberModifyDto>().ReverseMap();
            CreateMap<VisitorLogger, VisitorLoggerModifyDto>().ReverseMap();
            //VisitorLoggerModifyDto

        }
    }
}
