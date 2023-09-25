using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos;
using Nexus.Auth.Repository.Services.Interfaces;

namespace Nexus.Auth.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class VpcStorageController : ControllerBase
    {
        private readonly IVpcStorageService _requesterService;
        private readonly IConfiguration _configuration;

        public VpcStorageController(IVpcStorageService requesterService, IConfiguration configuration)
        {
            _requesterService = requesterService;
            _configuration = configuration;
        }

        /// GET: api/v1/VpcStorage/GetAll
        /// <summary>
        /// Endpoint to get all requesters
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll(PageParams pageParams) => Ok(await _requesterService.GetAll(pageParams, _configuration["ConnectionStrings:NexusVpcApi"]));

        /// POST: api/v1/VpcStorage/GetById
        /// <summary>
        /// Endpoint to get requester by id
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetById")]
        public async Task<IActionResult> GetById(GetById obj) => Ok(await _requesterService.GetById(obj, _configuration["ConnectionStrings:NexusVpcApi"]));

        /// POST: api/v1/VpcStorage/GetById
        /// <summary>
        /// Endpoint to get requesters by name
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetByName")]
        public async Task<IActionResult> GetByName(GetByName obj) => Ok(await _requesterService.GetByName(obj, _configuration["ConnectionStrings:NexusVpcApi"]));

        /// POST: api/v1/VpcStorage/Post
        /// <summary>
        /// Endpoint to create new requester
        /// </summary>
        /// <returns></returns>
        [HttpPost("Post")]
        public async Task<IActionResult> Post(VpcStorageDto obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Root.Errors);

            var response = await _requesterService.Post(obj, _configuration["ConnectionStrings:NexusVpcApi"]);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        /// POST: api/v1/VpcStorage/Put
        /// <summary>
        /// Endpoint to change an requester
        /// </summary>
        /// <returns></returns>
        [HttpPost("Put")]
        public async Task<IActionResult> Put(VpcStoragePutDto obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Root.Errors);

            var response = await _requesterService.Put(obj, _configuration["ConnectionStrings:NexusVpcApi"]);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        /// POST: api/v1/VpcStorage/Remove
        /// <summary>
        /// Endpoint to remove an requester
        /// </summary>
        /// <returns></returns>
        [HttpPost("Remove")]
        public async Task<IActionResult> Remove(GetById obj)
        {
            var response = await _requesterService.Delete(obj, _configuration["ConnectionStrings:NexusVpcApi"]);
            return response.Success ? Ok(response) : NotFound(response);
        }
    }
}
