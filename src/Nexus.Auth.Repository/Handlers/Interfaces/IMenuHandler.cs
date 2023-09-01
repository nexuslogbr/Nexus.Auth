using Nexus.Auth.Domain.Entities;

namespace Nexus.Auth.Repository.Handlers.Interfaces
{
    public interface IMenuHandler<T> : IBaseHandler<T> where T : class
    {
        Task<Menu> GetById(int id);
        Task<Menu> GetByName(string name);
    }
}
