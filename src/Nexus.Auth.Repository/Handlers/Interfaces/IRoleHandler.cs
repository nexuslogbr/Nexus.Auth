using Nexus.Auth.Domain.Entities;

namespace Nexus.Auth.Repository.Handlers.Interfaces
{
    public interface IRoleHandler<T> : IBaseHandler<T> where T : class
    {
        Task<Role> GetById(int id);
        Task<Role> GetByName(string name);
    }
}
