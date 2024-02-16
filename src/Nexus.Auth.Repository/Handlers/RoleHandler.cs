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

namespace Nexus.Auth.Repository.Handlers
{
    public class RoleHandler : IRoleHandler
    {
        private readonly IRoleService<Role> _roleService;
        private readonly IMenuService<Menu> _menuService;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        public RoleHandler(IRoleService<Role> roleService, IMenuService<Menu> menuService, RoleManager<Role> roleManager, IMapper mapper)
        {
            _roleService = roleService;
            _menuService = menuService;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<PageList<RoleModel>> GetAll(PageParams pageParams)
        {
            var roles = await _roleService.GetAllAsync(pageParams);
            var count = await _roleManager.Roles.CountAsync();
            return new PageList<RoleModel>(
                _mapper.Map<List<RoleModel>>(roles),
                count,
                pageParams.PageNumber,
                pageParams.PageSize);
        }

        public async Task<RoleModel> GetById(int id)
        {
            return _mapper.Map<RoleModel>(await _roleService.GetByIdAsync(id));
        }

        public async Task<RoleModel> GetByName(string name)
        {
            return _mapper.Map<RoleModel>(await _roleService.GetByNameAsync(name));
        }

        public async Task<RoleModel> Add(RoleDto dto)
        {
            var role = _mapper.Map<Role>(dto);
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
            var roleMenusToSave = entity.Menus.Select(menu => new RoleMenu { RoleId = role.Id, MenuId = menu.Id, RegisterDate = currentDate, ChangeDate = currentDate, Blocked = false }).ToList();
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
