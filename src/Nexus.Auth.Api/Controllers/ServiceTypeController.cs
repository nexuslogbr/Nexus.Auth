using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Services.Interfaces;
using Nexus.Auth.Repository.Dtos.ServiceType;

namespace Nexus.Auth.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class ServiceTypeController : ControllerBase
    {
        private readonly IServiceTypeService _serviceTypeService;
        private readonly IConfiguration _configuration;

        public ServiceTypeController(IServiceTypeService serviceTypeService, IConfiguration configuration)
        {
            _serviceTypeService = serviceTypeService;
            _configuration = configuration;
        }

        /// GET: api/v1/ServiceType/GetAll
        /// <summary>
        /// Endpoint to get all serviceTypes
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll(PageParams pageParams) => Ok(await _serviceTypeService.GetAll(pageParams, _configuration["ConnectionStrings:NexusVpcApi"]));

        /// POST: api/v1/ServiceType/GetById
        /// <summary>
        /// Endpoint to get serviceType by id
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetById")]
        public async Task<IActionResult> GetById(GetById obj) => Ok(await _serviceTypeService.GetById(obj, _configuration["ConnectionStrings:NexusVpcApi"]));

        /// POST: api/v1/ServiceType/Post
        /// <summary>
        /// Endpoint to create new serviceType
        /// </summary>
        /// <returns></returns>
        [HttpPost("Post")]
        public async Task<IActionResult> Post(ServiceTypeDto obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Root.Errors);

            var response = await _serviceTypeService.Post(obj, _configuration["ConnectionStrings:NexusVpcApi"]);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        /// POST: api/v1/ServiceType/Put
        /// <summary>
        /// Endpoint to change an serviceType
        /// </summary>
        /// <returns></returns>
        [HttpPost("Put")]
        public async Task<IActionResult> Put(ServiceTypePutDto obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Root.Errors);

            var response = await _serviceTypeService.Put(obj, _configuration["ConnectionStrings:NexusVpcApi"]);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        /// POST: api/v1/ServiceType/Remove
        /// <summary>
        /// Endpoint to remove an serviceType
        /// </summary>
        /// <returns></returns>
        [HttpPost("Remove")]
        public async Task<IActionResult> Remove(GetById obj)
        {
            var response = await _serviceTypeService.Delete(obj, _configuration["ConnectionStrings:NexusVpcApi"]);
            return response.Success ? Ok(response) : NotFound(response);
        }
    }
}
