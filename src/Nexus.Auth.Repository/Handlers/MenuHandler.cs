using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Infra.Context;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.Menu;
using Nexus.Auth.Repository.Handlers.Interfaces;
using Nexus.Auth.Repository.Models;
using Nexus.Auth.Repository.Services.Interfaces;

namespace Nexus.Auth.Repository.Handlers
{
    public class MenuHandler : IMenuHandler<Menu>
    {
        private readonly IMenuService<Menu> _menuService;
        private readonly NexusAuthContext _context;
        private readonly IMapper _mapper;

        public MenuHandler(IMenuService<Menu> menuService, NexusAuthContext context, IMapper mapper)
        {
            _menuService = menuService ?? throw new ArgumentNullException(nameof(menuService));
            _context = context;
            _mapper = mapper;
        }

        public async Task<PageList<MenuModel>> GetAll(PageParams pageParams)
        {
            var menus = await _menuService.GetAllAsync(pageParams);

            var result = menus.Select(m => new MenuModel
            {
                Id = m.Id,
                Name = m.Name,
                Mobile = m.Mobile,
                SubMenus = m.SubMenus.Select(sm => new SubMenuModel
                {
                    Name = sm.Name,
                    Link = sm.Link,
                    Mobile = sm.Mobile
                }).ToList(),
                RegisterDate = m.RegisterDate.ToString(),
                ChangeDate = m.ChangeDate.ToString()
            });

            var count = await _context.Menus.CountAsync();
            
            return new PageList<MenuModel>(
                _mapper.Map<List<MenuModel>>(menus),
                count,
                pageParams.PageNumber,
                pageParams.PageSize);
        }

        public async Task<MenuModel> GetById(int id)
        {
            var result = await _menuService.GetByIdAsync(id);

            if (result is not null)
            {
                return new MenuModel()
                {
                    Id = result.Id,
                    Name = result.Name,
                    Mobile = result.Mobile,
                    SubMenus = result.SubMenus.Select(sb => new SubMenuModel
                    {
                        Name= sb.Name,
                        Link = sb.Link,
                        Mobile = sb.Mobile
                    }).ToList()
                };
            }
            return null;
        }

        public async Task<MenuModel> GetByName(string name)
        {
            var result = await _menuService.GetByNameAsync(name);

            if (result is not null)
            {
                return new MenuModel()
                {
                    Id = result.Id,
                    Name = result.Name,
                    Mobile = result.Mobile,
                    SubMenus = result.SubMenus.Select(sb => new SubMenuModel
                    {
                        Name = sb.Name,
                        Link = sb.Link,
                        Mobile = sb.Mobile
                    }).ToList()
                };
            }
            return null;
        }

        public async Task<Menu> Add(Menu entity)
        {
            if (await _menuService.Add(entity))
            {
                return await _menuService.GetByNameAsync(entity.Name);
            }

            return null;
        }

        public async Task<Menu> Update(Menu entity)
        {
            var menu = await _menuService.GetByIdAsync(entity.Id);
            menu.Name = string.IsNullOrEmpty(entity.Name) ? menu.Name : entity.Name;
            menu.Mobile = entity.Mobile;
            menu.ChangeDate = DateTime.Now;

            if (await _menuService.Update(menu))
                return await _menuService.GetByNameAsync(entity.Name);

            return null;
        }

        public async Task<bool> Delete(int id)
        {
            var removed = await _menuService.Delete(id);

            if (removed)
                return true;

            return false;
        }

        public Task<Menu> DeleteRange(Menu[] entity)
        {
            throw new NotImplementedException();
        }

        public Task<Menu> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
