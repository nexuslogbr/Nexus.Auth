using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Repository.Dtos.Auth;
using Nexus.Auth.Repository.Dtos.Menu;
using Nexus.Auth.Repository.Dtos.Place;
using Nexus.Auth.Repository.Dtos.Role;
using Nexus.Auth.Repository.Dtos.User;
using Nexus.Auth.Repository.Handlers.Interfaces;
using Nexus.Auth.Repository.Models;
using Nexus.Auth.Repository.Services.Interfaces;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Handlers
{
    public class AuthHandler : IAuthHandler
    {
        private readonly IAuthService _authService;
        private readonly IUserService<User> _userService;
        private readonly IMapper _mapper;
        private readonly IPlaceService _placeService;
        private readonly IMenuService _menuService;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthHandler(
            IAuthService authService,
            IUserService<User> userService,
            IMapper mapper,
            IPlaceService placeService,
            IMenuService menuService,
            IJwtTokenService jwtTokenService)
        {
            _authService = authService;
            _userService = userService;
            _mapper = mapper;
            _placeService = placeService;
            _menuService = menuService;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<UserModel> Register(UserDto entity, int userId)
        {
            var user = _mapper.Map<User>(entity);
            var userLogged = await _userService.GetByIdAsync(userId);
            user.PlaceId = userLogged.PlaceId;
            var result = await _authService.Register(user, entity.Password);

            if (!result.Succeeded)
                throw new Exception(result.Errors.First().Code + ": " + result.Errors.First().Description);

            return _mapper.Map<UserModel>(user);
        }

        public async Task<UserModel> Update(UserPutDto entity)
        {
            var user = await _userService.GetByIdAsync(entity.Id);
            if (user is null)
                throw new Exception("Usuário não encontrado.");

            var updated = _mapper.Map(entity, user);
            updated.ChangeDate = DateTime.Now;

            await _userService.DeleteRoles(updated.Id);
            var result = !string.IsNullOrEmpty(entity.Password) ? await UpdateUserWithPass(updated, entity.Password) : await _authService.UpdateUserNoPass(updated);

            if (!result.Succeeded)
                throw new Exception("Erro ao salvar usuário.");

            return _mapper.Map<UserModel>(user);
        }

        public async Task<UserModel> UpdatePassword(UserDto entity)
        {
            var user = await _userService.GetByEmailAsync(entity.Email);
            if (user is null)
                throw new Exception("Usuário não encontrado.");

            user.ChangeDate = DateTime.Now;

            var result = !string.IsNullOrEmpty(entity.Password) ?
                await UpdateUserWithPass(user, entity.Password) :
                await _authService.UpdateUserNoPass(user);
            if (!result.Succeeded)
                throw new Exception("Erro ao salvar usuário.");

            return _mapper.Map<UserModel>(user);
        }

        public async Task<GenericCommandResult<object>> RegisterRolesToUser(List<UserRole> entity)
        {
            var rolesOfUser = new List<UserRole>();

            foreach (var item in entity)
            {
                var user = await _authService.FindUserByUserId(item.UserId);
                if (user is null)
                    return new GenericCommandResult<object>(false, "Non-existent user", new object { }, StatusCodes.Status404NotFound);

                var role = await _authService.FindRoleByRoleId(item.RoleId);
                if (role is null)
                    return new GenericCommandResult<object>(false, "Non-existent role", new object { }, StatusCodes.Status404NotFound);

                user.UserRoles = new List<UserRole> { new UserRole { User = user, Role = role } };
                var result = await _authService.RegisterRoles(user);

                if (result.Succeeded)
                    rolesOfUser.Add(new UserRole { Role = role, User = user });
                else
                    return new GenericCommandResult<object>(false, result.Errors.ToList()[0].Description, new object { }, StatusCodes.Status400BadRequest);

            }

            return new GenericCommandResult<object>(true, "Successful creation", true, StatusCodes.Status200OK);
        }

        public async Task<AuthResult> Login(UserLoginDto dto, bool isEmail, bool mobile)
        {
            User user = null;
            if (isEmail)
                user = await _authService.FindUserByEmail(dto.UserName);
            else if (!string.IsNullOrEmpty(dto.UserName))
                user = await _authService.FindUserByUserName(dto.UserName);

            if (user is null)
                return null;
            else
            {
                var userResult = _mapper.Map<UserResult>(user);

                userResult.Location = _mapper.Map<PlaceModel>(await _placeService.GetByIdAsync(user.PlaceId));
                userResult.ResetPasswordToken = await _userService.GeneratePasswordResetTokenAsync(user);

                var locations = user.UserPlaces.Select(_ => _.PlaceId).ToList();
                var places = await _placeService.GetByIdsAsync(locations);
                var menus = await _menuService.GetMenuByRoleIdAsync(user.Roles.First().Id);
                var result = await _authService.CheckPasswordSignIn(user, dto.Password);

                if (result.Succeeded)
                    return new AuthResult
                    {
                        Token = await _jwtTokenService.GenerateJWToken(user),
                        User = userResult,
                        Roles = _mapper.Map<RoleResult[]>(user.Roles),
                        Menus = _mapper.Map<MenuResult[]>(menus.Select(roleMenu => roleMenu.Menu).ToList()),
                        Places = _mapper.Map<PlaceResult[]>(places)
                    };
            }

            return null;
        }

        public async Task<bool> Logout()
        {
            var success = await _authService.Logout();

            if (success)
                return true;

            return false;
        }

        protected async Task<IdentityResult> UpdateUserWithPass(User user, string password)
        {
            return await _authService.UpdateUserWithPass(user, password);
        }
    }
}
