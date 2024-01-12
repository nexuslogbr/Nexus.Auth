using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.Place;
using Nexus.Auth.Repository.Services.Interfaces;

namespace Nexus.Auth.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class PlaceController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IPlaceService _placeService;

        public PlaceController(IConfiguration configuration, IPlaceService placeService)
        {
            _configuration = configuration;
            _placeService = placeService;
        }

        /// GET: api/v1/Place/GetAll
        /// <summary>
        /// Endpoint to get all Places
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll(PageParams pageParams) => Ok(await _placeService.GetAll(pageParams, _configuration["ConnectionStrings:NexusCustomerApi"]));

        /// POST: api/v1/Place/GetById
        /// <summary>
        /// Endpoint to get Place by id
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetById")]
        public async Task<IActionResult> GetById(GetById obj) => Ok(await _placeService.GetById(obj, _configuration["ConnectionStrings:NexusCustomerApi"]));

        /// POST: api/v1/Place/Post
        /// <summary>
        /// Endpoint to create new Place
        /// </summary>
        /// <returns></returns>
        [HttpPost("Post")]
        public async Task<IActionResult> Post(PlaceDto obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Root.Errors);

            var response = await _placeService.Post(obj, _configuration["ConnectionStrings:NexusCustomerApi"]);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        /// POST: api/v1/Place/Put
        /// <summary>
        /// Endpoint to change an Place
        /// </summary>
        /// <returns></returns>
        [HttpPost("Put")]
        public async Task<IActionResult> Put(PlacePutDto obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Root.Errors);

            var response = await _placeService.Put(obj, _configuration["ConnectionStrings:NexusCustomerApi"]);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        /// POST: api/v1/Place/Remove
        /// <summary>
        /// Endpoint to remove an Place
        /// </summary>
        /// <returns></returns>
        [HttpPost("Remove")]
        public async Task<IActionResult> Remove(GetById obj)
        {
            var response = await _placeService.Delete(obj, _configuration["ConnectionStrings:NexusCustomerApi"]);
            return response.Success ? Ok(response) : NotFound(response);
        }

        /// POST: api/v1/Place/ChangeStatus
        /// <summary>
        /// Endpoint to change the status for Place
        /// </summary>
        /// <returns></returns>
        [HttpPost("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus(ChangeStatusDto obj)
        {
            var response = await _placeService.ChangeStatus(obj, _configuration["ConnectionStrings:NexusCustomerApi"]);
            return response.Success ? Ok(response) : NotFound(response);
        }
    }
}
