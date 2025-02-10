using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.Role;
using Nexus.Auth.Repository.Handlers.Interfaces;
using Nexus.Auth.Repository.Models;
using Nexus.Auth.Repository.Services.Interfaces;
using Nexus.Auth.Repository.Utils;
using System.Data;

namespace Nexus.Auth.Repository.Handlers
{
    public class RoleHandler : IRoleHandler
    {
        private readonly IRoleService<Role> _roleService;
        private readonly IMenuService _menuService;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        public RoleHandler(IRoleService<Role> roleService, IMenuService menuService, RoleManager<Role> roleManager, IMapper mapper)
        {
            _roleService = roleService;
            _menuService = menuService;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<PageList<RoleModel>> GetAll(PageParams pageParams, int placeId)
        {
            var roles = await _roleService.GetAllAsync(pageParams, placeId);

            foreach (var role in roles)
            {
                role.Menus = new List<Menu>();
                foreach (var item in role.RoleMenus) 
                    role.Menus.Add(item.Menu);
            }

            var count = await _roleManager.Roles.Where(x => x.PlaceData == placeId).CountAsync();
            return new PageList<RoleModel>(
                _mapper.Map<List<RoleModel>>(roles),
                count,
                pageParams.PageNumber,
                pageParams.PageSize);
        }

        public async Task<RoleModel> GetById(int id)
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role is not null)
            {
                role.Menus = new List<Menu>();
                foreach (var item in role.RoleMenus)
                    role.Menus.Add(item.Menu);

                return _mapper.Map<RoleModel>(role);
            }

            return null;
        }

        public async Task<RoleModel> GetByName(string name)
        {
            var role = await _roleService.GetByNameAsync(name);
            if (role is not null)
            {
                role.Menus = new List<Menu>();
                foreach (var item in role.RoleMenus)
                    role.Menus.Add(item.Menu);

                return _mapper.Map<RoleModel>(role);
            }

            return null;
        }

        public async Task<RoleModel> Add(RoleDto dto, int placeId)
        {
            var role = _mapper.Map<Role>(dto);
            role.RoleMenus = new List<RoleMenu>();
            role.PlaceData = placeId;

            foreach (var menu in dto.Menus)
                role.RoleMenus.Add(new RoleMenu { MenuId = menu.Id, RegisterDate = DateTime.Now });

            var success = await _roleService.Add(role);
            if (!success)
                throw new Exception("Cargo inválido.");
            return _mapper.Map<RoleModel>(role);
        }

        public async Task<RoleModel> Update(RoleIdDto entity)
        {
            var role = await _roleService.GetByIdAsync(entity.Id);
            var menus = await _menuService.GetByRoleIdAsync(entity.Id);

            if (role is null)
                throw new Exception("Perfil inválido.");
            else if (menus.Count == 0)
                throw new Exception("Perfil sem menus vinculados.");

            var currentDate = DateTimeExtensions.GetCurrentDate();
            var roleMenusToSave = entity.Menus.Select(menu => new RoleMenu { 
                RoleId = role.Id,
                MenuId = menu.Id,
                ChangeDate = currentDate, 
            }).ToList();

            var roleMenusToRemove = await _menuService.GetMenuByRoleIdAsync(role.Id);

            if (await _menuService.AddRange(roleMenusToSave))
            {
                await _menuService.DeleteRange(roleMenusToRemove);
                var updated = _mapper.Map(entity, role);
                var response = await _roleService.Update(updated);
                if (!response)
                    throw new Exception("Erro ao salvar o perfil.");
            
                return _mapper.Map<RoleModel>(updated);
            }
            else
                throw new Exception("Menu inválido.");
        }

        public async Task<bool> Delete(int id)
        {
            return await _roleService.Delete(id);
        }

        public async Task<bool> ChangeStatus(ChangeStatusDto dto)
        {
            var user = await _roleService.GetByIdAsync(dto.Id);
            if (user is null) return false;

            return await _roleService.ChangeStatus(user, dto.Blocked);
        }
    }
}
