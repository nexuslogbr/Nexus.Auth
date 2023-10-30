using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.Role;
using Nexus.Auth.Repository.Models;

namespace Nexus.Auth.Repository.Handlers.Interfaces
{
    public interface IRoleHandler
    {
        Task<PageList<RoleModel>> GetAll(PageParams pageParams);
        Task<RoleModel> GetById(int id);
        Task<RoleModel> GetByName(string name);
        Task<RoleModel> Add(RoleDto entity);
        Task<RoleModel> Update(RoleIdDto entity);
        Task<bool> Delete(int id);
    }
}
