﻿using System.Web;
using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Repository.Handlers.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Auth.Repository.Dtos.Generics;
using Nexus.Auth.Api.Helpers;
using Nexus.Auth.Repository.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Nexus.Auth.Repository.Utils;
using Nexus.Auth.Repository.Dtos.User;
using Nexus.Auth.Repository.Dtos.Auth;

namespace Nexus.Auth.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthHandler _authHandler;
        private readonly IUserHandler _userHandler;
        private readonly IMapper _mapper;

        public AuthController(IAuthHandler authHandler, IUserHandler userHandler, IMapper mapper)
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
        public async Task<GenericCommandResult<UserModel>> Register(UserDto dto)
        {
            try
            {
                if (dto.Roles.Count == 0)
                    return new GenericCommandResult<UserModel>(true, "Role list is required", null, StatusCodes.Status204NoContent);

                var result = await _authHandler.Register(dto);
                return new GenericCommandResult<UserModel>(true, "User created", result, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new GenericCommandResult<UserModel>(true, "Query error" + ex.Message, null, StatusCodes.Status500InternalServerError);
            }
        }

        /// POST: api/v1/Authentication/Login
        /// <summary>
        /// Service to login of user
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<GenericCommandResult<AuthResult>> Login(UserLoginDto dto)
        {
            try
            {
                var result = await _authHandler.Login(dto, EmailChecker.IsValidEmail(dto.UserName));

                if (result is not null)
                    return new GenericCommandResult<AuthResult>(
                        true, 
                        "User logged", 
                        result, 
                        StatusCodes.Status200OK);

                return new GenericCommandResult<AuthResult>(false, "Invalid user or password", null, StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                return new GenericCommandResult<AuthResult>(false, "Invalids data" + ex.Message, null, StatusCodes.Status500InternalServerError);
            }
        }

        /// POST: api/v1/Authentication/Logout
        /// <summary>
        /// Service to logout of user
        /// </summary>
        /// <returns></returns>
        [HttpGet("Logout")]
        public async Task<GenericCommandResult<object>> Logout()
        {
            try
            {
                return new GenericCommandResult<object>(true, "Logout success", await _authHandler.Logout(), StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new GenericCommandResult<object>(true, "Invalids data" + ex.Message, new object { }, StatusCodes.Status400BadRequest);
            }
        }

        /// POST: api/v1/Authentication/RequestPasswordReset
        /// <summary>
        /// Service to request reset password
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("RequestPasswordReset")]
        public async Task<GenericCommandResult<object>> RequestPasswordReset(RequestResetPasswordDto dto)
        {
            var user = await _userHandler.GetByEmail(dto.Email);

            if (user is null)
                return new GenericCommandResult<object>(true, "User not found", null, StatusCodes.Status400BadRequest);

            await _userHandler.GeneratePasswordResetTokenAsync(user);
            return new GenericCommandResult<object>(true, "Success", null, StatusCodes.Status200OK);
        }

        /// POST: api/v1/Authentication/ResetPassword
        /// <summary>
        /// Service to reset password
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("ResetPassword")]
        public async Task<GenericCommandResult<IdentityResult>> ResetPassword(ResetPasswordDto dto)
        {
            var user = await _userHandler.GetByEmail(dto.Email);

            if (user is null)
                return new GenericCommandResult<IdentityResult>(true, "User not found", null, StatusCodes.Status400BadRequest);

            var decodeToken = dto.Logged ? dto.ResetPasswordToken: HttpUtility.UrlDecode(dto.ResetPasswordToken);
            var result = await _userHandler.ResetPasswordAsync(user, decodeToken, dto.NewPassword);

            if (!result.Succeeded)
                return new GenericCommandResult<IdentityResult>(false, result.Errors.FirstOrDefault().Description, result, StatusCodes.Status404NotFound);

            return new GenericCommandResult<IdentityResult>(true, "Success", result, StatusCodes.Status200OK);
        }
    }
}
