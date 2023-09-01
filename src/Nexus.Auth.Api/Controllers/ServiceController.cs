using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexus.Auth.Repository.Dtos;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Interfaces;

namespace Nexus.Api.Web.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class ServiceController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceService<ServiceDto> _serviceService;

        public ServiceController(IConfiguration configuration, IServiceService<ServiceDto> serviceService)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _serviceService = serviceService ?? throw new ArgumentNullException(nameof(serviceService));
        }

        /// GET: api/v1/Service/GetAll
        /// <summary>
        /// Endpoint to get all services
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll(PageParams pageParams) => Ok(await _serviceService.GetAll(pageParams, _configuration["ConnectionStrings:NexusVpcApi"]));

        /// POST: api/v1/Service/GetById
        /// <summary>
        /// Endpoint to get service by id
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetById")]
        public async Task<IActionResult> GetById(GetById obj) => Ok(await _serviceService.GetById(obj, _configuration["ConnectionStrings:NexusVpcApi"]));

        /// POST: api/v1/Service/Post
        /// <summary>
        /// Endpoint to create new service
        /// </summary>
        /// <returns></returns>
        [HttpPost("Post")]
        public async Task<IActionResult> Post(ServiceDto obj) => Ok(await _serviceService.Post(obj, _configuration["ConnectionStrings:NexusVpcApi"]));

        /// POST: api/v1/Service/Put
        /// <summary>
        /// Endpoint to change an service
        /// </summary>
        /// <returns></returns>
        [HttpPost("Put")]
        public async Task<IActionResult> Put(ServiceIdDto obj) => Ok(await _serviceService.Put(obj, _configuration["ConnectionStrings:NexusVpcApi"]));

        /// POST: api/v1/Service/Remove
        /// <summary>
        /// Endpoint to remove an service
        /// </summary>
        /// <returns></returns>
        [HttpPost("Remove")]
        public async Task<IActionResult> Remove(GetById obj) => Ok(await _serviceService.Delete(obj, _configuration["ConnectionStrings:NexusVpcApi"]));

    }
}
