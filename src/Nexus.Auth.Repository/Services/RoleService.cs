using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Repository.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Infra.Context;

namespace Nexus.Auth.Repository.Services
{
    public class RoleService : IRoleService<Role>
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly NexusAuthContext _context;
        private readonly IMenuService _menuService;

        public RoleService(RoleManager<Role> roleManager, NexusAuthContext context, IMenuService menuService)
        {
            _roleManager = roleManager;
            _context = context;
            _menuService = menuService;
        }

        public async Task<List<Role>> GetAllAsync(PageParams pageParams)
        {
            var query = _roleManager.Roles.AsQueryable();

            query = query
                .Where(x => x.Name.ToLower().Contains(pageParams.Term.ToLower()))
                .Include(x => x.RoleMenus)
                .ThenInclude(x => x.Menu);

            if (!string.IsNullOrEmpty(pageParams.OrderByProperty))
            {
                switch (pageParams.OrderByProperty.ToLower())
                {
                    case "name":
                        query = pageParams.Asc ? query.OrderBy(u => u.Name) : query.OrderByDescending(u => u.Name);
                        break;
                    case "description":
                        query = pageParams.Asc ? query.OrderBy(u => u.Description) : query.OrderByDescending(u => u.Description);
                        break;
                    case "changeDate":
                        query = pageParams.Asc ? query.OrderBy(u => u.ChangeDate) : query.OrderByDescending(u => u.ChangeDate);
                        break;
                    default:
                        query = query.OrderBy(u => u.Id);
                        break;
                }
            }
            else
                query = query.OrderBy(u => u.Id);

            var count = await _roleManager.Roles.CountAsync();
            var items = await query.Skip((pageParams.PageNumber - 1) * pageParams.PageSize).Take(pageParams.PageSize).ToListAsync();
            return items;
        }

        public async Task<Role> GetByIdAsync(int id)
        {
            var query = _roleManager.Roles.AsQueryable();

            query = query
                .Include(x => x.RoleMenus)
                .ThenInclude(x => x.Menu);

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Role> GetByNameAsync(string name)
        {
            var query = _roleManager.Roles.AsQueryable();

            query = query
                .Include(x => x.RoleMenus)
                .ThenInclude(x => x.Menu);

            return await query.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<IList<Role>> GetByUserIdAsync(int userId)
        {
            var userRoles = _context
                .UserRoles
                .Include(_ => _.Role)
                .ThenInclude(x => x.RoleMenus)
                .ThenInclude(x => x.Menu)
                .Where(x => x.UserId == userId);
            return await userRoles.Where(x => x.Role != null).Select(x => x.Role).ToListAsync();
        }

        public async Task<bool> Add(Role entity)
        {
            entity.RegisterDate = DateTime.Now;
            entity.ChangeDate = DateTime.Now;
            var result = await _roleManager.CreateAsync(entity);
            return result.Succeeded;
        }

        public async Task<IList<Menu>> AddMenus(List<RoleMenu> list)
        {
            await _context.RoleMenus.AddRangeAsync(list);

            if (await SaveChangesAsync())
            {
                var roleId = list.FirstOrDefault().RoleId;

                if (roleId > 0)
                    return await _menuService.GetByRoleIdAsync(roleId);
            }

            throw new Exception("Error saving or loaging entity");
        }

        public async Task<bool> Update(Role entity)
        {
            entity.ChangeDate = DateTime.Now;
            var result = await _roleManager.UpdateAsync(entity);
            return result.Succeeded;
        }

        public async Task<bool> Delete(int id)
        {
            var role = await GetByIdAsync(id);
            if (role is not null)
            {
                var result = await _roleManager.DeleteAsync(role);
                return result.Succeeded;
            }

            return false;
        }

        public async Task<bool> DeleteMenus(int roleId)
        {
            var roles = await _context.RoleMenus.Where(_ => _.RoleId == roleId).ToListAsync();

            if (roles.Count() > 0)
            {
                _context.RoleMenus.RemoveRange(roles);
                return await SaveChangesAsync();
            }

            return false;

            throw new Exception("Error saving or loaging entity");
        }

        public async Task<bool> ChangeStatus(Role role, bool blocked)
        {
            role.Blocked = blocked;
            var result = await _roleManager.UpdateAsync(role);
            return result.Succeeded;
        }

        public Task<bool> DeleteRange(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
