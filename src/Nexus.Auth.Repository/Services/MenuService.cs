using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Infra.Context;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Nexus.Auth.Repository.Services
{
    public class MenuService : IMenuService<Menu>
    {
        private readonly NexusAuthContext _context;
        
        public MenuService(NexusAuthContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Menu>> GetAllAsync(PageParams pageParams)
        {

            IQueryable<Menu> query = _context.Menus.AsQueryable();

            query = query
                .Where(x => x.Name.ToLower().Contains(pageParams.Term.ToLower()))
                .OrderBy(x => x.Id);

            var count = await _context.Menus.CountAsync();
            var items = await query.Skip((pageParams.PageNumber - 1) * pageParams.PageSize).Take(pageParams.PageSize).ToListAsync();
            return items;
        }

        public async Task<Menu> GetByIdAsync(int id)
        {
            return await _context.Menus.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IList<Menu>> GetByRoleIdAsync(int roleId)
        {
            var roleMenus = _context.RoleMenus.Include(_ => _.Menu).Where(x => x.RoleId == roleId);
            return await roleMenus.Where(x => x.Menu != null).Select(x => x.Menu).ToListAsync();
        }

        public async Task<IList<RoleMenu>> GetMenuByRoleIdAsync(int roleId)
        {
            return await _context.RoleMenus.Include(_ => _.Menu).Where(x => x.RoleId == roleId).ToListAsync();
        }

        public async Task<Menu> GetByNameAsync(string name)
        {
            return await _context.Menus.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<bool> Add(Menu entity)
        {
            var result = await _context.Menus.AddAsync(entity);
            return await SaveChangesAsync();
        }

        public async Task<bool> AddRange(IList<RoleMenu> menus)
        {
            await _context.RoleMenus.AddRangeAsync(menus);
            return await SaveChangesAsync();
        }

        public async Task<bool> Update(Menu entity)
        {
            var result = _context.Menus.Update(entity).Entity;
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

        public async Task<bool> DeleteRange(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteRange(IList<RoleMenu> menus)
        {
            _context.RoleMenus.RemoveRange(menus);
            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
