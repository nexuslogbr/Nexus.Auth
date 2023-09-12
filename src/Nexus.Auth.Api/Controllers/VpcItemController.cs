using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos;
using Nexus.Auth.Repository.Services.Interfaces;

namespace Nexus.Auth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VpcItemController : ControllerBase
    {
        private readonly IVpcItemService _vpcItemService;
        private readonly IConfiguration _configuration;

        public VpcItemController(IVpcItemService vpcItemService, IConfiguration configuration)
        {
            _vpcItemService = vpcItemService;
            _configuration = configuration;
        }

        /// GET: api/v1/VpcItem/GetAll
        /// <summary>
        /// Endpoint to get all vpcItems
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll(PageParams pageParams) => Ok(await _vpcItemService.GetAll(pageParams, _configuration["ConnectionStrings:NexusVpcApi"]));

        /// POST: api/v1/VpcItem/GetById
        /// <summary>
        /// Endpoint to get vpcItem by id
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetById")]
        public async Task<IActionResult> GetById(GetById obj) => Ok(await _vpcItemService.GetById(obj, _configuration["ConnectionStrings:NexusVpcApi"]));

        /// POST: api/v1/VpcItem/GetById
        /// <summary>
        /// Endpoint to get vpcItems by name
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetByName")]
        public async Task<IActionResult> GetByName(GetByName obj) => Ok(await _vpcItemService.GetByName(obj, _configuration["ConnectionStrings:NexusVpcApi"]));

        /// POST: api/v1/VpcItem/Post
        /// <summary>
        /// Endpoint to create new vpcItem
        /// </summary>
        /// <returns></returns>
        [HttpPost("Post")]
        public async Task<IActionResult> Post(VpcItemDto obj) => Ok(await _vpcItemService.Post(obj, _configuration["ConnectionStrings:NexusVpcApi"]));

        /// POST: api/v1/VpcItem/Put
        /// <summary>
        /// Endpoint to change an vpcItem
        /// </summary>
        /// <returns></returns>
        [HttpPost("Put")]
        public async Task<IActionResult> Put(VpcItemDto obj) => Ok(await _vpcItemService.Put(obj, _configuration["ConnectionStrings:NexusVpcApi"]));

        /// POST: api/v1/VpcItem/Remove
        /// <summary>
        /// Endpoint to remove an vpcItem
        /// </summary>
        /// <returns></returns>
        [HttpPost("Remove")]
        public async Task<IActionResult> Remove(GetById obj) => Ok(await _vpcItemService.Delete(obj, _configuration["ConnectionStrings:NexusVpcApi"]));
    }
}
