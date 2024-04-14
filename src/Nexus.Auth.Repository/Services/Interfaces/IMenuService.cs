using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Infra.Services.Interfaces;

namespace Nexus.Auth.Repository.Services.Interfaces
{
    public interface IMenuService : IBaseDataService<Menu>
    {
        Task<Menu> GetByIdAsync(int id);
        Task<Menu> GetByNameAsync(string name);
        Task<IList<Menu>> GetByRoleIdAsync(int roleId);
        Task<bool> AddRange(IList<RoleMenu> menus);
        Task<bool> DeleteRange(IList<RoleMenu> menus);
        Task<IList<RoleMenu>> GetMenuByRoleIdAsync(int roleId);
    }
}
