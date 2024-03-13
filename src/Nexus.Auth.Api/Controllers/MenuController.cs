using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Handlers.Interfaces;
using Nexus.Auth.Repository.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Nexus.Auth.Repository.Dtos.Menu;

namespace Nexus.Auth.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuHandler<Menu> _menuHandler;
        private readonly IMapper _mapper;

        public MenuController(IMenuHandler<Menu> menuHandler, IMapper mapper)
        {
            _menuHandler = menuHandler ?? throw new ArgumentNullException(nameof(menuHandler));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// GET: api/v1/Menu/GetAll
        /// <summary>
        /// Endpoint to get all menus
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll(PageParams pageParams)
        {
            try
            {
                return Ok(await _menuHandler.GetAll(pageParams));
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex + " Query error");
            }
        }

        /// POST: api/v1/Menu/GetById
        /// <summary>
        /// Endpoint to get menus by id
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetById")]
        public async Task<IActionResult> GetById(GetById dto)
        {
            try
            {
                var result = await _menuHandler.GetById(dto.Id);

                if (result is not null)
                    return Ok(result);

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex + " Query error");
            }
        }

        /// POST: api/v1/Menu/GetByName
        /// <summary>
        /// Endpoint to get menus by name
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetByName")]
        public async Task<IActionResult> GetByName(GetByName dto)
        {
            try
            {
                var result = await _menuHandler.GetByName(dto.Name);

                if (result is not null)
                    return Ok(result);

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex + " Query error");
            }
        }

        /// POST: api/v1/Menu/Post
        /// <summary>
        /// Endpoint to create new menus
        /// </summary>
        /// <returns></returns>
        [HttpPost("Post")]
        public async Task<IActionResult> Post(MenuDto dto)
        {
            try
            {
                var menu = _mapper.Map<Menu>(dto);
                var result = await _menuHandler.Add(menu);

                if (result is not null)
                    return Created("", _mapper.Map<MenuModel>(result));

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex + " Query error");
            }
        }

        /// POST: api/v1/Menu/Put
        /// <summary>
        /// Endpoint to change an menus
        /// </summary>
        /// <returns></returns>
        [HttpPost("Put")]
        public async Task<IActionResult> Put(MenuDto dto)
        {
            try
            {
                var role = _mapper.Map<Menu>(dto);
                var result = await _menuHandler.Update(role);

                if (result is not null)
                    return Ok(_mapper.Map<MenuModel>(result));

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex + " Query error");
            }
        }

        /// POST: api/v1/Menu/Remove
        /// <summary>
        /// Endpoint to remove an menus
        /// </summary>
        /// <returns></returns>
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(GetById dto)
        {
            try
            {
                var result = await _menuHandler.Delete(dto.Id);

                if (result)
                    return Ok(result);

                return NotFound(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex + " Query error");
            }
        }

    }
}
