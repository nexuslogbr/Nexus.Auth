using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Repository.Dtos;
using Nexus.Auth.Repository.Handlers.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Auth.Repository.Dtos.Generics;
using Nexus.Auth.Api.Helpers;
using Nexus.Auth.Repository.Models;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<IActionResult> Register(UserDto dto)
        {
            try
            {
                if (dto.Roles.Count == 0)
                    return this.StatusCode(StatusCodes.Status204NoContent, "Role list is required");

                var result = await _authHandler.Register(_mapper.Map<User>(dto), dto.Password);
                return Created("", _mapper.Map<UserModel>(result));
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex + " Query error");
            }
        }

        /// POST: api/v1/Authentication/Login
        /// <summary>
        /// Service to login of user
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            try
            {
                var result = await _authHandler.Login(dto, EmailChecker.IsValidEmail(dto.UserName));

                if (result is not null)
                    return Ok(result);

                return NotFound(new { message = "Usuário ou senha inválidos" });
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex + " Query error");
            }
        }

        /// POST: api/v1/Authentication/Logout
        /// <summary>
        /// Service to logout of user
        /// </summary>
        /// <returns></returns>
        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                return Ok(await _authHandler.Logout());
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex + " Query error");
            }
        }

        /// POST: api/v1/Authentication/RequestPasswordReset
        /// <summary>
        /// Service to request reset password
        /// </summary>
        /// <returns></returns>
        [HttpPost("RequestPasswordReset")]
        public async Task<IActionResult> RequestPasswordReset(RequestResetPasswordDto dto)
        {
            var user = await _userHandler.GetByEmail(dto.Email);

            if (user is null)
                return NotFound("Usuário não encontrado");

            var result = await _userHandler.GeneratePasswordResetTokenAsync(user);

            return Ok(result);
        }

        /// POST: api/v1/Authentication/ResetPassword
        /// <summary>
        /// Service to reset password
        /// </summary>
        /// <returns></returns>
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            var user = await _userHandler.GetByEmail(dto.Email);

            if (user is null)
                return NotFound("Usuário não encontrado");

            var token = await _userHandler.ResetPasswordAsync(user, dto.Token, dto.Password);

            return Ok("Token de redefinição de senha enviado");
        }
    }
}
