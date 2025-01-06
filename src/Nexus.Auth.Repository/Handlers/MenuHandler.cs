using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Infra.Context;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.Menu;
using Nexus.Auth.Repository.Handlers.Interfaces;
using Nexus.Auth.Repository.Models;
using Nexus.Auth.Repository.Params;
using Nexus.Auth.Repository.Services.Interfaces;

namespace Nexus.Auth.Repository.Handlers
{
    public class MenuHandler : IMenuHandler
    {
        private readonly IMenuService _menuService;
        private readonly NexusAuthContext _context;
        private readonly IMapper _mapper;

        public MenuHandler(IMenuService menuService, NexusAuthContext context, IMapper mapper)
        {
            _menuService = menuService ?? throw new ArgumentNullException(nameof(menuService));
            _context = context;
            _mapper = mapper;
        }

        public async Task<PageList<MenuModel>> GetAll(MenuParams pageParams)
        {
            var menus = await _menuService.GetAsync(
                pageParams.PageNumber, pageParams.pageSize, pageParams.Filter(), pageParams.OrderByProperty, pageParams.Asc, includeProps: "");

            var result = menus.Select(m => new MenuModel
            {
                Id = m.Id,
                Name = m.Name,
                Mobile = m.Mobile,
                Link = m.Link,
                Type = m.Type,
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
                return _mapper.Map<MenuModel>(result);

            return null;
        }

        public async Task<MenuModel> GetByName(string name)
        {
            var result = await _menuService.GetByNameAsync(name);

            if (result is not null)
                return _mapper.Map<MenuModel>(result);

            return null;
        }

        public async Task<MenuModel> Add(MenuDto entity)
        {
            var menu = await _menuService.AddAsync(_mapper.Map<Menu>(entity));
            await _menuService.SaveChangesAsync();
            return _mapper.Map<MenuModel>(menu);
        }

        public async Task<MenuModel> Update(MenuPutDto entity)
        {
            var menu = await _menuService.GetByIdAsync(entity.Id);
            if (menu is null)
                throw new Exception("Menu não encontrado.");

            var updated = _mapper.Map(entity, menu);
            var response = _menuService.Update(updated);

            
            await _menuService.SaveChangesAsync();
            return _mapper.Map<MenuModel>(response);
        }

        public async Task<bool> Delete(int id)
        {
            await _menuService.DeleteByIdAsync(id);
            return await _menuService.SaveChangesAsync();
        }

        public Task<Menu> DeleteRange(Menu[] entity)
        {
            throw new NotImplementedException();
        }

        public Task<Menu> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ChangeStatusDto> ChangeStatus(ChangeStatusDto dto)
        {
            var menu = await _menuService.GetByIdAsync(dto.Id);
            if (menu is null)
                throw new Exception("Menu não encontrado.");
            _menuService.ChangeStatus(menu, dto.Blocked);
            await _menuService.SaveChangesAsync();
            return dto;
        }
    }
}
