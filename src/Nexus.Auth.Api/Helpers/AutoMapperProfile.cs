using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Models;
using AutoMapper;
using Nexus.Auth.Repository.Dtos.User;
using Nexus.Auth.Repository.Dtos.Role;
using Nexus.Auth.Repository.Dtos.Menu;
using Nexus.Auth.Repository.Dtos.Place;

namespace Nexus.Auth.Api.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>()
                .ReverseMap()
                .ForMember(d => d.Roles, src => src.Ignore())
                .ForMember(d => d.UserRoles, src => src.MapFrom(e => e.Roles))
                .ForMember(d => d.UserPlaces, src => src.MapFrom(e => e.Places));
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<User, GetById>().ReverseMap();
            CreateMap<User, UserResult>().ReverseMap();
            CreateMap<User, UserPlaceModel>().ReverseMap();
            CreateMap<User, GetAllUserModel>().ReverseMap();

            CreateMap<Role, RoleIdDto>().ReverseMap();
            CreateMap<Role, GetById>().ReverseMap();
            CreateMap<Role, RoleResult>().ReverseMap();
            CreateMap<UserRole, GetById>()
                .ReverseMap()
                .ForMember(d => d.Id, src => src.Ignore())
                .ForMember(d => d.RoleId, src => src.MapFrom(e => e.Id));
            CreateMap<Role, RoleModel>()
                .ReverseMap();
            CreateMap<Role, RoleDto>()
                .ReverseMap();
            
            CreateMap<RoleMenu, RoleMenuDto>().ReverseMap();
            CreateMap<RoleMenu, GetById>()
                .ReverseMap()
                .ForMember(e => e.MenuId, src => src.MapFrom(d => d.Id))
                .ForMember(e => e.Id, src => src.Ignore());

            CreateMap<UserRole, GetById>()
                .ReverseMap()
                .ForMember(d => d.Id, src => src.Ignore())
                .ForMember(d => d.RoleId, src => src.MapFrom(e => e.Id));
            CreateMap<UserRole, UserDto>().ReverseMap();
            
            CreateMap<Menu, MenuDto>().ReverseMap();
            CreateMap<Menu, MenuModel>().ReverseMap();
            CreateMap<Menu, MenuResult>().ReverseMap();

            CreateMap<Menu, GetById>().ReverseMap();

            CreateMap<PlaceModel, GetById>()
                .ReverseMap()
                .ForMember(d => d.Id, src => src.Ignore())
                .ForMember(d => d.Id, src => src.MapFrom(e => e.Id));
            CreateMap<UserPlace, GetById>()
                .ReverseMap()
                .ForMember(d => d.Id, src => src.Ignore())
                .ForMember(d => d.PlaceId, src => src.MapFrom(e => e.Id));

            CreateMap<Place, PlaceDto>().ReverseMap();
            CreateMap<Place, GetById>().ReverseMap();
            CreateMap<Place, PlaceModel>().ReverseMap();
            CreateMap<Place, PlaceResult>().ReverseMap();
        }
    }
}
