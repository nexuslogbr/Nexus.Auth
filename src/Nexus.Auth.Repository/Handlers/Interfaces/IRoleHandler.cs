using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Models;

namespace Nexus.Auth.Repository.Handlers.Interfaces
{
    public interface IRoleHandler<T> : IBaseHandler<T> where T : class
    {
        Task<PageList<RoleModel>> GetAll(PageParams pageParams);
        Task<Role> GetById(int id);
        Task<Role> GetByName(string name);
    }
}
