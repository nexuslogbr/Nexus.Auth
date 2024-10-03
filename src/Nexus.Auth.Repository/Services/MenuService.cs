using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Infra.Context;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Nexus.Auth.Infra.Services;

namespace Nexus.Auth.Repository.Services
{
    public class MenuService : BaseDataService<Menu>, IMenuService
    {
        public MenuService(NexusAuthContext context) : base(context) { }

        public async Task<List<Menu>> GetAllAsync(PageParams pageParams)
        {

            IQueryable<Menu> query = _context.Menus
                .AsQueryable();

            query = query
                .Where(x => x.Name.ToLower().Contains(pageParams.Term.ToLower()));

            if (!string.IsNullOrEmpty(pageParams.OrderByProperty))
            {
                switch (pageParams.OrderByProperty.ToLower())
                {
                    case "name":
                        query = pageParams.Asc ? query.OrderBy(u => u.Name) : query.OrderByDescending(u => u.Name);
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

            var count = await _context.Menus.CountAsync();
            var items = await query.Skip((pageParams.PageNumber - 1) * pageParams.PageSize).Take(pageParams.PageSize).ToListAsync();
            return items;
        }

        public async Task<Menu> GetByIdAsync(int id)
        {
            return await _context.Menus
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IList<Menu>> GetByRoleIdAsync(int roleId)
        {
            var roleMenus = _context.RoleMenus.Include(_ => _.Menu).Where(x => x.RoleId == roleId);
            return await roleMenus.Where(x => x.Menu != null).Select(x => x.Menu).ToListAsync();
        }

        public async Task<IList<RoleMenu>> GetMenuByRoleIdAsync(int roleId)
        {
            return await _context.RoleMenus
                .Include(_ => _.Menu)
                .Where(x => x.RoleId == roleId)
                .OrderBy(x => x.Menu.Order)
                .ToListAsync();
        }

        public async Task<Menu> GetByNameAsync(string name)
        {
            return await _context.Menus
                .FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<bool> AddRange(IList<RoleMenu> menus)
        {
            await _context.RoleMenus.AddRangeAsync(menus);
            return await SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity is not null)
            {
                _context.Menus.Remove(entity);
                return await SaveChangesAsync();
            }

            return false;
        }

        public async Task<bool> DeleteRange(IList<RoleMenu> menus)
        {
            _context.RoleMenus.RemoveRange(menus);
            return await SaveChangesAsync();
        }
    }
}
