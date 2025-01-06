using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.Menu;
using Nexus.Auth.Repository.Models;
using Nexus.Auth.Repository.Params;

namespace Nexus.Auth.Repository.Handlers.Interfaces
{
    public interface IMenuHandler
    {
        Task<MenuModel> Add(MenuDto entity);
        Task<MenuModel> Update(MenuPutDto entity);
        Task<bool> Delete(int id);
        Task<PageList<MenuModel>> GetAll(MenuParams pageParams);
        Task<MenuModel> GetById(int id);
        Task<MenuModel> GetByName(string name);
        Task<ChangeStatusDto> ChangeStatus(ChangeStatusDto dto);
    }
}
