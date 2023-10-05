using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Services.Interfaces;
using Nexus.Auth.Repository.Dtos.Requester;

namespace Nexus.Auth.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class RequesterController : ControllerBase
    {
        private readonly IRequesterService _requesterService;
        private readonly IConfiguration _configuration;

        public RequesterController(IRequesterService requesterService, IConfiguration configuration)
        {
            _requesterService = requesterService;
            _configuration = configuration;
        }
        
        /// GET: api/v1/Requester/GetAll
        /// <summary>
        /// Endpoint to get all requesters
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll(PageParams pageParams) => Ok(await _requesterService.GetAll(pageParams, _configuration["ConnectionStrings:NexusVpcApi"]));

        /// POST: api/v1/Requester/GetById
        /// <summary>
        /// Endpoint to get requester by id
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetById")]
        public async Task<IActionResult> GetById(GetById obj) => Ok(await _requesterService.GetById(obj, _configuration["ConnectionStrings:NexusVpcApi"]));

        /// POST: api/v1/Requester/Post
        /// <summary>
        /// Endpoint to create new requester
        /// </summary>
        /// <returns></returns>
        [HttpPost("Post")]
        public async Task<IActionResult> Post(RequesterDto obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Root.Errors);

            var response = await _requesterService.Post(obj, _configuration["ConnectionStrings:NexusVpcApi"]);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        /// POST: api/v1/Requester/Put
        /// <summary>
        /// Endpoint to change an requester
        /// </summary>
        /// <returns></returns>
        [HttpPost("Put")]
        public async Task<IActionResult> Put(RequesterPutDto obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Root.Errors);

            var response = await _requesterService.Put(obj, _configuration["ConnectionStrings:NexusVpcApi"]);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        /// POST: api/v1/Requester/Remove
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
