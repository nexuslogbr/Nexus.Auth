using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nexus.Auth.Repository.Dtos.Chassis;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Services.Interfaces;

namespace Nexus.Auth.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class ChassisController : ControllerBase
    {
        private readonly IChassisService _chassisService;
        private readonly IConfiguration _configuration;

        public ChassisController(IChassisService chassisService, IConfiguration configuration)
        {
            _chassisService = chassisService;
            _configuration = configuration;
        }

        /// GET: api/v1/Chassis/GetAll
        /// <summary>
        /// Endpoint to get all chassis
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll(PageParams pageParams) => Ok(await _chassisService.GetAll(pageParams, _configuration["ConnectionStrings:NexusVehicleApi"]));

        /// POST: api/v1/Chassis/GetById
        /// <summary>
        /// Endpoint to get chassis by id
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetById")]
        public async Task<IActionResult> GetById(GetById obj) => Ok(await _chassisService.GetById(obj, _configuration["ConnectionStrings:NexusVehicleApi"]));

        /// POST: api/v1/Chassis/GetBySerialNumber
        /// <summary>
        /// Endpoint to get chassis by serial number
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetBySerialNumber")]
        public async Task<IActionResult> GetBySerialNumber(GetBySerialNumber obj) => Ok(await _chassisService.GetBySerialNumber(obj, _configuration["ConnectionStrings:NexusVehicleApi"]));
    }
}
