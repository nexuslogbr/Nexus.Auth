using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Repository.Dtos;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Handlers.Interfaces;
using Nexus.Auth.Repository.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Nexus.Auth.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleHandler<Role> _roleHandler;
        private readonly IMapper _mapper;

        public RoleController(IRoleHandler<Role> roleHandler, IMapper mapper)
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
        public async Task<IActionResult> GetAll(PageParams pageParams)
        {
            try
            {
                var result = await _roleHandler.GetAll(pageParams);

                if (result.Count > 0)
                    return Ok(_mapper.Map<RoleModel[]>(result));

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex + " Query error");
            }
        }

        /// POST: api/v1/Role/GetById
        /// <summary>
        /// Endpoint to get roles by id
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetById")]
        public async Task<IActionResult> GetById(GetById dto)
        {
            try
            {
                var result = await _roleHandler.GetById(dto.Id);

                if (result is not null)
                    return Ok(_mapper.Map<RoleModel>(result));

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex + " Query error");
            }
        }

        /// POST: api/v1/Role/GetByName
        /// <summary>
        /// Endpoint to get roles by name
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetByName")]
        public async Task<IActionResult> GetByName(GetByName dto)
        {
            try
            {
                var result = await _roleHandler.GetByName(dto.Name);

                if (result is not null)
                    return Ok(_mapper.Map<RoleModel>(result));

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex + " Query error");
            }
        }

        /// POST: api/v1/Role/Post
        /// <summary>
        /// Endpoint to create new roles
        /// </summary>
        /// <returns></returns>
        [HttpPost("Post")]
        public async Task<IActionResult> Post(RoleDto dto)
        {
            try
            {
                if (dto.Menus.Count == 0)
                    return this.StatusCode(StatusCodes.Status204NoContent, "Menus list is required");

                return Created("", _mapper.Map<RoleModel>(await _roleHandler.Add(_mapper.Map<Role>(dto))));
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex + " Query error");
            }
        }

        /// POST: api/v1/Role/Put
        /// <summary>
        /// Endpoint to change an roles
        /// </summary>
        /// <returns></returns>
        [HttpPost("Put")]
        public async Task<IActionResult> Put(RoleIdDto dto)
        {
            try
            {
                return Ok(_mapper.Map<RoleModel>(await _roleHandler.Update(_mapper.Map<Role>(dto))));
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex + " Query error");
            }
        }

        /// POST: api/v1/Role/Remove
        /// <summary>
        /// Endpoint to remove an roles
        /// </summary>
        /// <returns></returns>
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(GetById dto)
        {
            try
            {
                var result = await _roleHandler.Delete(dto.Id);

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
