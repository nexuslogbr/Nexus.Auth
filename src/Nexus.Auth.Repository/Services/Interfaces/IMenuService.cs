using Nexus.Auth.Domain.Entities;

namespace Nexus.Auth.Repository.Services.Interfaces
{
    public interface IMenuService<T> : IBaseService<T> where T : class
    {
        Task<Menu> GetByIdAsync(int id);
        Task<Menu> GetByNameAsync(string name);
        Task<IList<Menu>> GetByRoleIdAsync(int roleId);
    }
}
