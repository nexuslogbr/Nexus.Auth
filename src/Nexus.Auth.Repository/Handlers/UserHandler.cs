using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Repository.Handlers.Interfaces;
using Nexus.Auth.Repository.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Nexus.Auth.Repository.Dtos.Generics;
using System.Data;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Handlers
{
    public class UserHandler : IUserHandler<User>
    {
        private readonly IUserService<User> _userService;
        private readonly IRoleService<Role> _roleService;

        public UserHandler(IUserService<User> userService, IRoleService<Role> roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }
       
        async Task<IList<User>> IBaseHandler<User>.GetAll(PageParams pageParams)
        {
            var users = await _userService.GetAllAsync(pageParams);

            foreach (var user in users)
                user.Roles = await _roleService.GetByUserIdAsync(user.Id);

            return users;
        }

        public async Task<User> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            user.Roles = user is not null ? await _roleService.GetByUserIdAsync(user.Id) : throw new Exception("Error load entity");
            return user;
        }

        public async Task<User> GetByName(string name)
        {
            var user = await _userService.GetByNameAsync(name);
            user.Roles = user is not null ? await _roleService.GetByUserIdAsync(user.Id) : throw new Exception("Error load entity");
            return user;
        }

        async Task<User> IUserHandler<User>.GetByEmail(string email)
        {
            var user = await _userService.GetByEmailAsync(email);
            user.Roles = user is not null ? await _roleService.GetByUserIdAsync(user.Id) : throw new Exception("Error load entity");
            return user;
        }

        public async Task<User> Add(User entity)
        {
            if (await _userService.Add(entity))
            {
                var user = await _userService.GetByNameAsync(entity.Name);
                user.Roles = await _userService.AddRoles(
                    entity.Roles.Select(role =>
                                                new UserRole
                                                {
                                                    RoleId = role.Id,
                                                    UserId = user.Id
                                                }).ToList()
                );

                return user;
            }

            throw new Exception("Error saving of entity");
        }

        public async Task<User> Update(User entity)
        {
            var user = await _userService.GetByIdAsync(entity.Id);
            user.Name = entity.Name = string.IsNullOrEmpty(entity.Name) ? user.Name : entity.Name;
            user.UserName = string.IsNullOrEmpty(entity.UserName) ? user.UserName : entity.UserName;
            user.Email = string.IsNullOrEmpty(entity.Email) ? user.Email : entity.Email;
            user.ChangeDate = DateTime.Now;

            if (await _userService.Update(entity))
            {
                var result = await _userService.GetByNameAsync(entity.Name);

                if (await _userService.DeleteRoles(user.Id))
                {
                    var users = entity.Roles.Select(role => new UserRole { RoleId = role.Id, UserId = result.Id }).ToList();
                    result.Roles = await _userService.AddRoles(users);
                }

                return result;
            }

            throw new Exception("Error update of entity");
        }

        async Task<bool> IBaseHandler<User>.Delete(int id)
        {
            var removed = await _userService.Delete(id);

            if (removed)
                return true;

            return false;
        }
        
        public Task<GenericCommandResult> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> DeleteRange(User[] entity)
        {
            throw new NotImplementedException();
        }

        Task<User> IBaseHandler<User>.SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<string> GeneratePasswordResetTokenAsync(User user)
        {
            return await _userService.GeneratePasswordResetTokenAsync(user);
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
    }
}
