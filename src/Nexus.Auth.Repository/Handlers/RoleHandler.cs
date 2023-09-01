using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Repository.Handlers.Interfaces;
using Nexus.Auth.Repository.Services.Interfaces;
using Nexus.Auth.Repository.Dtos.Generics;

namespace Nexus.Auth.Repository.Handlers
{
    public class RoleHandler : IRoleHandler<Role>
    {
        private readonly IRoleService<Role> _roleService;
        private readonly IMenuService<Menu> _menuService;

        public RoleHandler(IRoleService<Role> roleService, IMenuService<Menu> menuService) 
        {
            _roleService = roleService;
            _menuService = menuService;
        }

        async Task<IList<Role>> IBaseHandler<Role>.GetAll(PageParams pageParams)
        {
            var roles = await _roleService.GetAllAsync(pageParams);
            
            foreach (var role in roles)
                role.Menus = await _menuService.GetByRoleIdAsync(role.Id);    

            return roles;
        }

        async Task<Role> IRoleHandler<Role>.GetById(int id)
        {
            var role = await _roleService.GetByIdAsync(id);
            role.Menus = role is not null ? await _menuService.GetByRoleIdAsync(role.Id) : throw new Exception("Error load entity");
            return role;
        }

        async Task<Role> IRoleHandler<Role>.GetByName(string name)
        {
            var role = await _roleService.GetByNameAsync(name);
            role.Menus = role is not null ? await _menuService.GetByRoleIdAsync(role.Id) : throw new Exception("Error load entity");
            return role;
        }

        public async Task<Role> Add(Role entity)
        {
            if (await _roleService.Add(entity))
            {
                var role = await _roleService.GetByNameAsync(entity.Name);
                role.Menus = await _roleService.AddMenus(
                    entity.Menus.Select(menu => 
                                                new RoleMenu 
                                                {
                                                    MenuId = menu.Id, 
                                                    RoleId = role.Id 
                                                }).ToList()
                );

                return role;
            }

            throw new Exception("Error saving of entity");
        }

        public async Task<Role> Update(Role entity)
        {
            var role = await _roleService.GetByIdAsync(entity.Id);
            role.Name = entity.Name;
            role.Description = entity.Description;
            role.ChangeDate = DateTime.Now;

            if (await _roleService.Update(role))
            {
                var result = await _roleService.GetByNameAsync(entity.Name);

                if (await _roleService.DeleteMenus(role.Id))
                {
                    var menus = entity.Menus.Select(menu => new RoleMenu { MenuId = menu.Id, RoleId = result.Id }).ToList();
                    result.Menus = await _roleService.AddMenus(menus);
                }

                return result;
                throw new Exception("Error update of entity");
            }

            throw new Exception("Error update of entity");
        }

        async Task<bool> IBaseHandler<Role>.Delete(int id)
        {
            var removed = await _roleService.Delete(id);

            if (removed)
                return true;

            return false;
        }

        public Task<Role> DeleteRange(Role[] entity)
        {
            throw new NotImplementedException();
        }

        Task<Role> IBaseHandler<Role>.SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
