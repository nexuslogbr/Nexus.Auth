using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Models;

namespace Nexus.Auth.Repository.Handlers.Interfaces
{
    public interface IMenuHandler<T> : IBaseHandler<T> where T : class
    {
        Task<PageList<MenuModel>> GetAll(PageParams pageParams);
        Task<Menu> GetById(int id);
        Task<Menu> GetByName(string name);
    }
}
