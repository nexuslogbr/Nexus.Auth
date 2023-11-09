using Nexus.Auth.Domain.Entities;

namespace Nexus.Auth.Repository.Services.Interfaces
{
    public interface IRoleService<T> : IBaseService<T> where T : class
    {
        Task<Role> GetByIdAsync(int id);
        Task<Role> GetByNameAsync(string name);
        Task<IList<Role>> GetByUserIdAsync(int userId);
        Task<IList<Menu>> AddMenus(List<RoleMenu> list);
        Task<bool> DeleteMenus(int roleId);
        Task<bool> ChangeStatus(Role user, bool blocked);
    }
}
