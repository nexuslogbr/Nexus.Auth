using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Models;
using AutoMapper;
using Microsoft.AspNetCore.Routing.Constraints;
using Nexus.Auth.Repository.Dtos.User;
using Nexus.Auth.Repository.Dtos.Role;
using Nexus.Auth.Repository.Dtos.Menu;

namespace Nexus.Auth.Api.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>()
                .ReverseMap()
                .ForMember(d => d.Roles, src => src.Ignore())
                .ForMember(d => d.UserRoles, src => src.MapFrom(e => e.Roles));
            CreateMap<User, UserIdDto>().ReverseMap();
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<User, GetById>().ReverseMap();

            CreateMap<Role, RoleIdDto>().ReverseMap();
            CreateMap<Role, RoleUserDto>().ReverseMap();
            CreateMap<Role, RoleModel>()
                .ForMember(d => d.Menus, src => src.MapFrom(e => e.RoleMenus))
                .ReverseMap();
            CreateMap<Role, RoleDto>()
                .ReverseMap()
                .ForMember(e => e.RoleMenus, src => src.MapFrom(d => d.Menus));
            
            CreateMap<RoleMenu, RoleMenuDto>().ReverseMap();
            CreateMap<RoleMenu, GetById>()
                .ReverseMap()
                .ForMember(e => e.MenuId, src => src.MapFrom(d => d.Id))
                .ForMember(e => e.Id, src => src.Ignore());

            CreateMap<UserRole, UserRoleDto>()
                .ReverseMap()
                .ForMember(d => d.Id, src => src.Ignore())
                .ForMember(d => d.RoleId, src => src.MapFrom(e => e.Id));
            CreateMap<UserRole, UserDto>().ReverseMap();
            
            CreateMap<Menu, MenuDto>().ReverseMap();
            CreateMap<Menu, MenuIdDto>().ReverseMap();
            CreateMap<Menu, MenuModel>().ReverseMap();
            CreateMap<Menu, GetById>().ReverseMap();

            CreateMap<RoleMenu, MenuIdDto>()
                .ForMember(e => e.Name, src => src.MapFrom(d => d.Menu.Name))
                .ForMember(e => e.Mobile, src => src.MapFrom(d => d.Menu.Mobile))
                .ForMember(e => e.Id, src => src.MapFrom(d => d.MenuId));

        }
    }
}
