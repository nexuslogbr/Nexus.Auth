using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos;
using Nexus.Auth.Repository.Handlers.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nexus.Auth.Repository.Models;
using Microsoft.AspNetCore.Authorization;
using Nexus.Auth.Repository.Utils;

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
        public async Task<GenericCommandResult> GetAll(PageParams pageParams)
        {
            try
            {
                var result = await _userHandler.GetAll(pageParams);

                if (result.Count > 0)
                    return new GenericCommandResult(true, "Success", _mapper.Map<UserModel[]>(result), StatusCodes.Status200OK);

                return new GenericCommandResult(false, "No data", null, StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                return new GenericCommandResult(false, "Query error" + ex.Message, null, StatusCodes.Status500InternalServerError);
            }
        }

        /// POST: api/v1/User/GetById
        /// <summary>
        /// Endpoint to get user by id
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetById")]
        public async Task<GenericCommandResult> GetById(GetById dto)
        {
            try
            {
                var result = await _userHandler.GetById(dto.Id);

                if (result is not null)
                    return new GenericCommandResult(true, "Success", _mapper.Map<UserModel>(result), StatusCodes.Status200OK);

                return new GenericCommandResult(false, "No data", null, StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                return new GenericCommandResult(false, "Query error" + ex.Message, null, StatusCodes.Status500InternalServerError);
            }
        }

        /// POST: api/v1/User/GetByName
        /// <summary>
        /// Endpoint to get user by name
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetByName")]
        public async Task<GenericCommandResult> GetByName(GetByName dto)
        {
            try
            {
                var result = await _userHandler.GetByName(dto.Name);

                if (result is not null)
                    return new GenericCommandResult(true, "Success", _mapper.Map<UserModel>(result), StatusCodes.Status200OK);

                return new GenericCommandResult(false, "No data", null, StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                return new GenericCommandResult(false, "Query error" + ex.Message, null, StatusCodes.Status500InternalServerError);
            }
        }

        /// POST: api/v1/User/Post
        /// <summary>
        /// Endpoint to create new user
        /// </summary>
        /// <returns></returns>
        [HttpPost("Post")]
        public async Task<GenericCommandResult> Post(UserDto dto)
        {
            try
            {
                if (dto.Roles.Count == 0)
                    return new GenericCommandResult(true, "Role list is required", new object { }, StatusCodes.Status204NoContent);

                return new GenericCommandResult(
                    true,
                    "Success",
                    _mapper.Map<UserModel>(
                        await _authHandler.Register(_mapper.Map<User>(dto), dto.Password)
                    ),
                    StatusCodes.Status200OK
                );
            }
            catch (Exception ex)
            {
                return new GenericCommandResult(false, "Query error" + ex.Message, null, StatusCodes.Status500InternalServerError);
            }
        }

        /// POST: api/v1/User/Put
        /// <summary>
        /// Endpoint to change an user
        /// </summary>
        /// <returns></returns>
        [HttpPost("Put")]
        public async Task<GenericCommandResult> Put(UserIdDto dto)
        {
            try
            {
                return new GenericCommandResult(
                    true,
                    "Success",
                    _mapper.Map<UserModel>(await _authHandler.Update(_mapper.Map<User>(dto), dto.Password)),
                    StatusCodes.Status200OK
                );
            }
            catch (Exception ex)
            {
                return new GenericCommandResult(false, "Query error" + ex.Message, null, StatusCodes.Status500InternalServerError);
            }
        }

        /// POST: api/v1/User/Remove
        /// <summary>
        /// Endpoint to remove an user
        /// </summary>
        /// <returns></returns>
        [HttpPost("Delete")]
        public async Task<GenericCommandResult> Delete(GetById dto)
        {
            try
            {
                var result = await _userHandler.Delete(dto.Id);

                if (result)
                    return new GenericCommandResult(true, "Removed", result, StatusCodes.Status200OK);

                return new GenericCommandResult(false, "Error in saving", null, StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                return new GenericCommandResult(false, "Query error" + ex.Message, null, StatusCodes.Status500InternalServerError);
            }
        }

    }
}
