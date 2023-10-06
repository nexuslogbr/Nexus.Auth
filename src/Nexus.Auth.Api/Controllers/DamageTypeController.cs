using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos;
using Nexus.Auth.Repository.Services.Interfaces;

namespace Nexus.Auth.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DamageTypeController : ControllerBase
    {
        private readonly IDamageTypeService _damageTypeService;
        private readonly IConfiguration _configuration;

        public DamageTypeController(IDamageTypeService damageTypeService, IConfiguration configuration)
        {
            _damageTypeService = damageTypeService;
            _configuration = configuration;
        }

        /// GET: api/v1/DamageType/GetAll
        /// <summary>
        /// Endpoint to get all damageTypes
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll(PageParams pageParams) => Ok(await _damageTypeService.GetAll(pageParams, _configuration["ConnectionStrings:NexusVehicleApi"]));

        /// POST: api/v1/DamageType/GetById
        /// <summary>
        /// Endpoint to get damageType by id
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetById")]
        public async Task<IActionResult> GetById(GetById obj) => Ok(await _damageTypeService.GetById(obj, _configuration["ConnectionStrings:NexusVehicleApi"]));

        /// POST: api/v1/DamageType/Post
        /// <summary>
        /// Endpoint to create new damageType
        /// </summary>
        /// <returns></returns>
        [HttpPost("Post")]
        public async Task<IActionResult> Post(DamageTypeDto obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Root.Errors);

            var response = await _damageTypeService.Post(obj, _configuration["ConnectionStrings:NexusVehicleApi"]);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        /// POST: api/v1/DamageType/Put
        /// <summary>
        /// Endpoint to change an damageType
        /// </summary>
        /// <returns></returns>
        [HttpPost("Put")]
        public async Task<IActionResult> Put(DamageTypePutDto obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Root.Errors);

            var response = await _damageTypeService.Put(obj, _configuration["ConnectionStrings:NexusVehicleApi"]);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        /// POST: api/v1/DamageType/Remove
        /// <summary>
        /// Endpoint to remove an damageType
        /// </summary>
        /// <returns></returns>
        [HttpPost("Remove")]
        public async Task<IActionResult> Remove(GetById obj)
        {
            var response = await _damageTypeService.Delete(obj, _configuration["ConnectionStrings:NexusVehicleApi"]);
            return response.Success ? Ok(response) : NotFound(response);
        }
    }
}
