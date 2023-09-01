using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos;
using Nexus.Auth.Repository.Handlers.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nexus.Auth.Repository.Models;
using Microsoft.AspNetCore.Authorization;

namespace Nexus.Auth.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserHandler<User> _userHandler;
        private readonly IRoleHandler<Role> _roleHandler;
        private readonly IAuthHandler _authHandler;
        private readonly IMapper _mapper;

        public UserController(IUserHandler<User> userHandler, IRoleHandler<Role> roleHandler, IAuthHandler authHandler, IMapper mapper)
        {
            _userHandler = userHandler;
            _roleHandler = roleHandler;
            _authHandler = authHandler;
            _mapper = mapper;
        }

        /// GET: api/v1/User/GetAll
        /// <summary>
        /// Endpoint to get all users
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll(PageParams pageParams)
        {
            try
            {
                var result = await _userHandler.GetAll(pageParams);

                if (result.Count > 0)
                    return Ok(_mapper.Map<UserModel[]>(result));

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex + " Query error");
            }
        }

        /// POST: api/v1/User/GetById
        /// <summary>
        /// Endpoint to get user by id
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetById")]
        public async Task<IActionResult> GetById(GetById dto)
        {
            try
            {
                var result = await _userHandler.GetById(dto.Id);

                if (result is not null)
                    return Ok(_mapper.Map<UserModel>(result));

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex + " Query error");
            }
        }

        /// POST: api/v1/User/GetByName
        /// <summary>
        /// Endpoint to get user by name
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetByName")]
        public async Task<IActionResult> GetByName(GetByName dto)
        {
            try
            {
                var result = await _userHandler.GetByName(dto.Name);

                if (result is not null)
                    return Ok(_mapper.Map<UserModel>(result));

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex + " Query error");
            }
        }

        /// POST: api/v1/User/Post
        /// <summary>
        /// Endpoint to create new user
        /// </summary>
        /// <returns></returns>
        [HttpPost("Post")]
        public async Task<IActionResult> Post(UserDto dto)
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

        /// POST: api/v1/User/Put
        /// <summary>
        /// Endpoint to change an user
        /// </summary>
        /// <returns></returns>
        [HttpPost("Put")]
        public async Task<IActionResult> Put(UserIdDto dto)
        {
            try
            {
                var result = await _authHandler.Update(_mapper.Map<User>(dto), dto.Password);

                return Ok(_mapper.Map<UserModel>(result));
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex + " Query error");
            }
        }

        /// POST: api/v1/User/Remove
        /// <summary>
        /// Endpoint to remove an user
        /// </summary>
        /// <returns></returns>
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(GetById dto)
        {
            try
            {
                var result = await _userHandler.Delete(dto.Id);

                if (result)
                    return Ok(result);

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex + " Query error");
            }
        }

    }
}
