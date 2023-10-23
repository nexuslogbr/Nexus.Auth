using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Models;
using AutoMapper;
using Nexus.Auth.Repository.Dtos.User;
using Nexus.Auth.Repository.Dtos.Role;
using Nexus.Auth.Repository.Dtos.Menu;

namespace Nexus.Auth.Api.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserIdDto>().ReverseMap();
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<User[], UserDto[]>().ReverseMap();
            CreateMap<User[], GetById[]>().ReverseMap();

            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<Role, RoleIdDto>().ReverseMap();
            CreateMap<Role, RoleUserDto>().ReverseMap();
            CreateMap<Role, RoleModel>().ReverseMap();
            CreateMap<Role[], RoleDto[]>().ReverseMap();
            
            CreateMap<RoleMenuDto, RoleMenu>().ReverseMap();
            CreateMap<RoleMenu[], RoleMenuDto[]>().ReverseMap();
            
            CreateMap<UserRole, UserRoleDto>().ReverseMap();
            CreateMap<UserRole[], UserRoleDto[]>().ReverseMap();
            CreateMap<UserRole, UserDto>().ReverseMap();
            CreateMap<UserRole[], UserDto[]>().ReverseMap();
            
            CreateMap<Menu, MenuDto>().ReverseMap();
            CreateMap<Menu, MenuIdDto>().ReverseMap();
            CreateMap<Menu, MenuModel>().ReverseMap();
            CreateMap<Menu, GetById>().ReverseMap();
            CreateMap<Menu[], MenuDto[]>().ReverseMap();
            CreateMap<Menu[], MenuIdDto[]>().ReverseMap();
            CreateMap<Menu[], GetById[]>().ReverseMap();
        }
    }
}
