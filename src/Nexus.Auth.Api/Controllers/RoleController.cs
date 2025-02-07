using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Handlers.Interfaces;
using Nexus.Auth.Repository.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Nexus.Auth.Repository.Utils;
using Nexus.Auth.Repository.Dtos.Role;
using Nexus.Auth.Repository.Handlers;

namespace Nexus.Auth.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleHandler _roleHandler;
        private readonly IMapper _mapper;

        public RoleController(IRoleHandler roleHandler, IMapper mapper)
        {
            _roleHandler = roleHandler;
            _mapper = mapper;
        }

        /// GET: api/v1/Role/GetAll
        /// <summary>
        /// Endpoint to get all roles
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetAll")]
        public async Task<GenericCommandResult<PageList<RoleModel>>> GetAll(PageParams pageParams)
        {
            try
            {
                var result = await _roleHandler.GetAll(pageParams, int.Parse(User?.FindFirst("PlaceId")?.Value));

                if (result.TotalCount > 0)
                    return new GenericCommandResult<PageList<RoleModel>>(true, "Success", result, StatusCodes.Status200OK);

                return new GenericCommandResult<PageList<RoleModel>>(false, "No data", null, StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                return new GenericCommandResult<PageList<RoleModel>>(false, "Query error" + ex.Message, null, StatusCodes.Status500InternalServerError);
            }
        }

        /// POST: api/v1/Role/GetById
        /// <summary>
        /// Endpoint to get roles by id
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetById")]
        public async Task<GenericCommandResult<RoleModel>> GetById(GetById dto)
        {
            try
            {
                var result = await _roleHandler.GetById(dto.Id);

                if (result is not null)
                    return new GenericCommandResult<RoleModel>(true, "Success", result, StatusCodes.Status200OK);

                return new GenericCommandResult<RoleModel>(false, "No data", null, StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                return new GenericCommandResult<RoleModel>(false, "Query error" + ex.Message, null, StatusCodes.Status500InternalServerError);
            }
        }

        /// POST: api/v1/Role/GetByName
        /// <summary>
        /// Endpoint to get roles by name
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetByName")]
        public async Task<GenericCommandResult<RoleModel>> GetByName(GetByName dto)
        {
            try
            {
                var result = await _roleHandler.GetByName(dto.Name);

                if (result is not null)
                    return new GenericCommandResult<RoleModel>(true, "Success", result, StatusCodes.Status200OK);

                return new GenericCommandResult<RoleModel>(false, "No data", null, StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                return new GenericCommandResult<RoleModel>(false, "Query error" + ex.Message, null, StatusCodes.Status500InternalServerError);
            }
        }

        /// POST: api/v1/Role/Post
        /// <summary>
        /// Endpoint to create new roles
        /// </summary>
        /// <returns></returns>
        [HttpPost("Post")]
        public async Task<GenericCommandResult<RoleModel>> Post(RoleDto dto)
        {
            try
            {
                if (dto.Menus.Count == 0)
                    return new GenericCommandResult<RoleModel>(true, "Menus list is required", null, StatusCodes.Status204NoContent);

                return new GenericCommandResult<RoleModel>(true, "Success", await _roleHandler.Add(dto), StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new GenericCommandResult<RoleModel>(false, "Query error" + ex.Message, null, StatusCodes.Status500InternalServerError);
            }
        }

        /// POST: api/v1/Role/Put
        /// <summary>
        /// Endpoint to change an roles
        /// </summary>
        /// <returns></returns>
        [HttpPost("Put")]
        public async Task<GenericCommandResult<RoleModel>> Put(RoleIdDto dto)
        {
            try
            {
                return new GenericCommandResult<RoleModel>(true, "Success", await _roleHandler.Update(dto), StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new GenericCommandResult<RoleModel>(false, ex.Message, null, StatusCodes.Status500InternalServerError);
            }
        }

        /// POST: api/v1/Role/Remove
        /// <summary>
        /// Endpoint to remove an roles
        /// </summary>
        /// <returns></returns>
        [HttpPost("Delete")]
        public async Task<GenericCommandResult<object>> Delete(GetById dto)
        {
            try
            {
                var result = await _roleHandler.Delete(dto.Id);

                if (result)
                    return new GenericCommandResult<object>(true, "Removed", result, StatusCodes.Status200OK);

                return new GenericCommandResult<object>(false, "Error in saving", null, StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                return new GenericCommandResult<object>(false, "Query error" + ex.Message, null, StatusCodes.Status500InternalServerError);
            }
        }

        /// POST: api/v1/Role/ChangeStatus
        /// <summary>
        /// Endpoint to change the status for role
        /// </summary>
        /// <returns></returns>
        [HttpPost("ChangeStatus")]
        public async Task<GenericCommandResult<object>> ChangeStatus(ChangeStatusDto obj)
        {
            try
            {
                var result = await _roleHandler.ChangeStatus(obj);

                if (result)
                    return new GenericCommandResult<object>(true, "Updated", result, StatusCodes.Status200OK);

                return new GenericCommandResult<object>(false, "Error in saving", null, StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                return new GenericCommandResult<object>(false, "Query error" + ex.Message, null, StatusCodes.Status500InternalServerError);
            }
        }

    }
}
