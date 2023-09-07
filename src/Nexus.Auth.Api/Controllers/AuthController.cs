using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Repository.Dtos;
using Nexus.Auth.Repository.Handlers.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Auth.Repository.Dtos.Generics;
using Nexus.Auth.Api.Helpers;
using Nexus.Auth.Repository.Models;
using Microsoft.AspNetCore.Authorization;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthHandler _authHandler;
        private readonly IUserHandler<User> _userHandler;
        private readonly IMapper _mapper;

        public AuthController(IAuthHandler authHandler, IUserHandler<User> userHandler, IMapper mapper)
        {
            _authHandler = authHandler;
            this._userHandler = userHandler;
            _mapper = mapper;
        }

        /// POST: api/v1/Authentication/Register
        /// <summary>
        /// Endpoint to create new user
        /// </summary>
        /// <returns></returns>
        [HttpPost("Register")]
        public async Task<GenericCommandResult> Register(UserDto dto)
        {
            try
            {
                if (dto.Roles.Count == 0)
                    return new GenericCommandResult(true, "Role list is required", new object { }, StatusCodes.Status204NoContent);

                var result = await _authHandler.Register(_mapper.Map<User>(dto), dto.Password);
                return new GenericCommandResult(true, "User created", result, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new GenericCommandResult(true, "Query error" + ex.Message, new object { }, StatusCodes.Status500InternalServerError);
            }
        }

        /// POST: api/v1/Authentication/Login
        /// <summary>
        /// Service to login of user
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<GenericCommandResult> Login(UserLoginDto dto)
        {
            try
            {
                var result = await _authHandler.Login(dto, EmailChecker.IsValidEmail(dto.UserName));

                if (result is not null)
                    return new GenericCommandResult(true, "User logged", result, StatusCodes.Status200OK);

                return new GenericCommandResult(true, "Invalid user or password", new object { }, StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                return new GenericCommandResult(true, "Invalids data" + ex.Message, new object { }, StatusCodes.Status400BadRequest);
            }
        }

        /// POST: api/v1/Authentication/Logout
        /// <summary>
        /// Service to logout of user
        /// </summary>
        /// <returns></returns>
        [HttpGet("Logout")]
        public async Task<GenericCommandResult> Logout()
        {
            try
            {
                return new GenericCommandResult(true, "Logout success", await _authHandler.Logout(), StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new GenericCommandResult(true, "Invalids data" + ex.Message, new object { }, StatusCodes.Status400BadRequest);
            }
        }

        /// POST: api/v1/Authentication/RequestPasswordReset
        /// <summary>
        /// Service to request reset password
        /// </summary>
        /// <returns></returns>
        [HttpPost("RequestPasswordReset")]
        public async Task<GenericCommandResult> RequestPasswordReset(RequestResetPasswordDto dto)
        {
            var user = await _userHandler.GetByEmail(dto.Email);

            if (user is null)
                return new GenericCommandResult(true, "User not found", new object { }, StatusCodes.Status400BadRequest);

            var result = await _userHandler.GeneratePasswordResetTokenAsync(user);
            return new GenericCommandResult(true, "Success", result, StatusCodes.Status200OK);
        }

        /// POST: api/v1/Authentication/ResetPassword
        /// <summary>
        /// Service to reset password
        /// </summary>
        /// <returns></returns>
        [HttpPost("ResetPassword")]
        public async Task<GenericCommandResult> ResetPassword(ResetPasswordDto dto)
        {
            var user = await _userHandler.GetByEmail(dto.Email);

            if (user is null)
                return new GenericCommandResult(true, "User not found", new object { }, StatusCodes.Status400BadRequest);

            var token = await _userHandler.ResetPasswordAsync(user, dto.Token, dto.Password);

            return new GenericCommandResult(true, "Success", token, StatusCodes.Status200OK);
        }
    }
}
