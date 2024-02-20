using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.User;
using Nexus.Auth.Repository.Handlers.Interfaces;
using Nexus.Auth.Repository.Models;
using Nexus.Auth.Repository.Services.Interfaces;
using Nexus.Auth.Repository.Utils;
using System.Web;

namespace Nexus.Auth.Repository.Handlers
{
    public class UserHandler : IUserHandler
    {
        private readonly IUserService<User> _userService;
        private readonly IRoleService<Role> _roleService;
        private readonly UserManager<User> _userManager;
        private readonly ISmtpMailService _smtpMailService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IPlaceService _placeService;

        public UserHandler(
            IUserService<User> userService,
            IRoleService<Role> roleService,
            IMapper mapper,
            UserManager<User> userManager,
            ISmtpMailService smtpMailService,
            IConfiguration configuration,
            IPlaceService placeService)
        {
            _userService = userService;
            _roleService = roleService;
            _mapper = mapper;
            _userManager = userManager;
            _smtpMailService = smtpMailService;
            _configuration = configuration;
            _placeService = placeService;
        }

        public async Task<PageList<UserModel>> GetAll(PageParams pageParams)
        {
            var users = await _userService.GetAllAsync(pageParams);

            foreach (var user in users)
                user.Roles = await _roleService.GetByUserIdAsync(user.Id);

            var count = await _userManager.Users.CountAsync();

            return new PageList<UserModel>(
                _mapper.Map<List<UserModel>>(users),
                count,
                pageParams.PageNumber,
                pageParams.PageSize);
        }

        public async Task<User> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user is null) return null;

            user.Roles = user is not null ? await _roleService.GetByUserIdAsync(user.Id) : throw new Exception("Error load entity");
            return user;
        }

        public async Task<User> GetByName(string name)
        {
            var user = await _userService.GetByNameAsync(name);
            user.Roles = user is not null ? await _roleService.GetByUserIdAsync(user.Id) : throw new Exception("Error load entity");
            return user;
        }

        async Task<User> IUserHandler.GetByEmail(string email)
        {
            var user = await _userService.GetByEmailAsync(email);
            user.Roles = user is not null ? await _roleService.GetByUserIdAsync(user.Id) : throw new Exception("Error load entity");
            return user;
        }

        public async Task<UserModel> Add(UserDto entity)
        {
            var user = _mapper.Map<User>(entity);

            var place = (await _placeService.GetById(entity.PlaceId)).Data;
            if (place is null) throw new Exception("Local inválido");

            user.PlaceId = place.Id;
            user.PlaceName = place.Name;
            user.ChangeDate = DateTime.Now;
            user.RegisterDate = DateTime.Now;
            var result = await _userService.Add(user);

            if (result)
                throw new Exception("Error saving of entity");

            return _mapper.Map<UserModel>(user);
        }

        public async Task<UserModel> Update(UserIdDto entity)
        {
            var user = await _userService.GetByIdAsync(entity.Id);
            await _userService.DeleteRoles(user.Id);
            var updated = _mapper.Map(entity, user);

            var place = (await _placeService.GetById(entity.PlaceId)).Data;
            if (place is null)  throw new Exception("Local inválido");

            updated.PlaceId = place.Id;
            updated.PlaceName = place.Name;
            updated.ChangeDate = DateTime.Now;
            var result = await _userService.Update(updated);
            
            if (result)
                throw new Exception("Error update of entity");
            
            return _mapper.Map<UserModel>(updated);
        }

        public async Task<bool> Delete(int id)
        {
            return await _userService.Delete(id);
        }

        public async Task GeneratePasswordResetTokenAsync(User user)
        {
            var token = await _userService.GeneratePasswordResetTokenAsync(user);
            var resetLink = GeneratePasswordResetLink(user.Email, token);
            var settings = _configuration.GetSection("MailSettings").Get<MailSettings>();
            var data = new MailData
            {
                Body = $"<h3>Recuperação de Senha!</h3>" +
               $"<p>Olá</p>" +
               $"<p>Clique no link para redefinir a senha:</p>" +
               $"<a href=\"{resetLink}\">Redefinir Senha</a>",
                From = settings.EmailFrom,
                Subject = "Alteração de senha",
                To = user.Email
            };
            await _smtpMailService.SendMailAsync(data);
        }

        public async Task<IdentityResult> ResetPasswordAsync(User user, string token, string password)
        {
            return await _userService.ResetPasswordAsync(user, token, password);
        }

        public async Task<bool> AddRoleByUser(User user, Role role)
        {
            if (user is null || role is null)
                return false;

            user.UserRoles = new List<UserRole> { new UserRole { User = user, Role = role } };
            var result = await _userService.RegisterRoles(user);

            if (result.Succeeded)
                return true;

            return false;
        }

        public async Task<bool> UpdateRoleByUser(User user, Role role)
        {
            if (user is null || role is null)
                return false;

            var roles = await GetRolesByUser(user);
            if (!roles.Contains(role.Name))
            {
                user.UserRoles = new List<UserRole> { new UserRole { User = user, Role = role } };
                var result = await _userService.RegisterRoles(user);

                if (result.Succeeded)
                    return true;
            }

            return false;
        }

        public async Task<IList<string>> GetRolesByUser(User user)
        {
            return await _userService.GetRolesByIdAsync(user);
        }

        public async Task<IdentityResult> RemoveFromRoles(User user, IList<string> userRoles)
        {
            return await _userService.RemoveRoles(user, userRoles);
        }

        public async Task<bool> ChangeStatus(ChangeStatusDto dto)
        {
            var user = await _userService.GetByIdAsync(dto.Id);
            if (user is null) return false;

            return await _userService.ChangeStatus(user, dto.Blocked);
        }

        private string GeneratePasswordResetLink(string email, string token)
        {
            var path = _configuration["AppSettings:ResetPassword"];
            return $"{path}/{HttpUtility.UrlEncode(email)}/{HttpUtility.UrlEncode(token)}";
        }
    }
}
