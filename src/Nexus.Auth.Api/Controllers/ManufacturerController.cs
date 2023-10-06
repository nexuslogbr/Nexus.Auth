using Microsoft.AspNetCore.Mvc;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos;
using Nexus.Auth.Repository.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Nexus.Auth.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/[controller]")]
    public class ManufacturerController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IManufacturerService _manufacturerService;

        public ManufacturerController(IConfiguration configuration, IManufacturerService manufacturerService)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _manufacturerService = manufacturerService ?? throw new ArgumentNullException(nameof(manufacturerService));

        }

        /// GET: api/v1/Manufacturer/GetAll
        /// <summary>
        /// Endpoint to get all manufacturers
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll(PageParams pageParams) => Ok(await _manufacturerService.GetAll(pageParams, _configuration["ConnectionStrings:NexusVehicleApi"]));

        /// POST: api/v1/Manufacturer/GetById
        /// <summary>
        /// Endpoint to get manufacturer by id
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetById")]
        public async Task<IActionResult> GetById(GetById obj) => Ok(await _manufacturerService.GetById(obj, _configuration["ConnectionStrings:NexusVehicleApi"]));

        /// POST: api/v1/Manufacturer/Post
        /// <summary>
        /// Endpoint to create new manufacturer
        /// </summary>
        /// <returns></returns>
        [HttpPost("Post")]
        public async Task<IActionResult> Post(ManufacturerDto obj) => Ok(await _manufacturerService.Post(obj, _configuration["ConnectionStrings:NexusVehicleApi"]));

        /// POST: api/v1/Manufacturer/Put
        /// <summary>
        /// Endpoint to change an manufacturer
        /// </summary>
        /// <returns></returns>
        [HttpPost("Put")]
        public async Task<IActionResult> Put(ManufacturerIdDto obj) => Ok(await _manufacturerService.Put(obj, _configuration["ConnectionStrings:NexusVehicleApi"]));

        /// POST: api/v1/Manufacturer/Remove
        /// <summary>
        /// Endpoint to remove an manufacturer
        /// </summary>
        /// <returns></returns>
        [HttpPost("Remove")]
        public async Task<IActionResult> Remove(GetById obj) => Ok(await _manufacturerService.Delete(obj, _configuration["ConnectionStrings:NexusVehicleApi"]));

    }
}
