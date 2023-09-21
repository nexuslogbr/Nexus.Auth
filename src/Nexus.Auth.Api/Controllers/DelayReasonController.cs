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
    public class DelayReasonController : ControllerBase
    {
        private readonly IDelayReasonService _delayReasonService;
        private readonly IConfiguration _configuration;

        public DelayReasonController(IDelayReasonService delayReasonService, IConfiguration configuration)
        {
            _delayReasonService = delayReasonService;
            _configuration = configuration;
        }

        /// GET: api/v1/DelayReason/GetAll
        /// <summary>
        /// Endpoint to get all delayReasons
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll(PageParams pageParams) => Ok(await _delayReasonService.GetAll(pageParams, _configuration["ConnectionStrings:NexusVpcApi"]));

        /// POST: api/v1/DelayReason/GetById
        /// <summary>
        /// Endpoint to get delayReason by id
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetById")]
        public async Task<IActionResult> GetById(GetById obj) => Ok(await _delayReasonService.GetById(obj, _configuration["ConnectionStrings:NexusVpcApi"]));

        /// POST: api/v1/DelayReason/GetByName
        /// <summary>
        /// Endpoint to get delayReasons by name
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetByName")]
        public async Task<IActionResult> GetByName(GetByName obj) => Ok(await _delayReasonService.GetByName(obj, _configuration["ConnectionStrings:NexusVpcApi"]));

        /// POST: api/v1/DelayReason/Post
        /// <summary>
        /// Endpoint to create new delayReason
        /// </summary>
        /// <returns></returns>
        [HttpPost("Post")]
        public async Task<IActionResult> Post(DelayReasonDto obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Root.Errors);

            var response = await _delayReasonService.Post(obj, _configuration["ConnectionStrings:NexusVpcApi"]);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        /// POST: api/v1/DelayReason/Put
        /// <summary>
        /// Endpoint to change an delayReason
        /// </summary>
        /// <returns></returns>
        [HttpPost("Put")]
        public async Task<IActionResult> Put(DelayReasonPutDto obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Root.Errors);

            var response = await _delayReasonService.Put(obj, _configuration["ConnectionStrings:NexusVpcApi"]);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        /// POST: api/v1/DelayReason/Remove
        /// <summary>
        /// Endpoint to remove an delayReason
        /// </summary>
        /// <returns></returns>
        [HttpPost("Remove")]
        public async Task<IActionResult> Remove(GetById obj)
        {
            var response = await _delayReasonService.Delete(obj, _configuration["ConnectionStrings:NexusVpcApi"]);
            return response.Success ? Ok(response) : NotFound(response);
        }
    }
}
