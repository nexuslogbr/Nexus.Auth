using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.Place;
using Nexus.Auth.Repository.Handlers.Interfaces;
using Nexus.Auth.Repository.Params;

namespace Nexus.Auth.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class PlaceController : ControllerBase
    {
        private readonly IPlaceHandler _placeHandler;

        public PlaceController(IPlaceHandler placeHandler)
        {
            _placeHandler = placeHandler;
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll(PlaceParams pageParams)
        {
            try
            {
                return Ok(await _placeHandler.GetAll(pageParams));
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex + " Query error");
            }
        }

        [HttpPost("GetById")]
        public async Task<IActionResult> GetById(GetById dto)
        {
            try
            {
                var result = await _placeHandler.GetById(dto.Id);

                if (result is not null)
                    return Ok(result);

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex + " Query error");
            }
        }

        [HttpPost("GetByIds")]
        public async Task<IActionResult> GetByIds(List<int> dto)
        {
            try
            {
                var result = await _placeHandler.GetByIds(dto);

                if (result is not null)
                    return Ok(result);

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex + " Query error");
            }
        }

        [HttpPost("GetByName")]
        public async Task<IActionResult> GetByName(GetByName dto)
        {
            try
            {
                var result = await _placeHandler.GetByName(dto.Name);

                if (result is not null)
                    return Ok(result);

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex + " Query error");
            }
        }

        [HttpPost("Post")]
        public async Task<IActionResult> Post(PlaceDto dto)
        {
            try
            {
                return Created("", await _placeHandler.Add(dto));
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex + " Query error");
            }
        }

        [HttpPost("Put")]
        public async Task<IActionResult> Put(PlacePutDto dto)
        {
            try
            {
                if (dto.Id <= 0)
                    return this.StatusCode(StatusCodes.Status404NotFound, " Id doesn't exists");

                return Ok(await _placeHandler.Update(dto));
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex + " Query error");
            }
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(GetById dto)
        {
            try
            {
                var result = await _placeHandler.Delete(dto.Id);

                if (result)
                    return Ok(result);

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex + " Query error");
            }
        }

        [HttpPost("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus(ChangeStatusDto dto)
        {
            try
            {
                return Ok(await _placeHandler.ChangeStatus(dto));
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex + " Query error");
            }
        }
    }
}
