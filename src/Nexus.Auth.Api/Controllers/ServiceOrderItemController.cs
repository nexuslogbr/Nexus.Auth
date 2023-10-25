using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.ServiceOrder;
using Nexus.Auth.Repository.Dtos.ServiceOrderItem;
using Nexus.Auth.Repository.Params;
using Nexus.Auth.Repository.Services;
using Nexus.Auth.Repository.Services.Interfaces;

namespace Nexus.Auth.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class ServiceOrderItemController : ControllerBase
    {
        private readonly IServiceOrderItemService _serviceOrderItemService;
        private readonly IConfiguration _configuration;

        public ServiceOrderItemController(IServiceOrderItemService serviceOrderItemService, IConfiguration configuration)
        {
            _serviceOrderItemService = serviceOrderItemService;
            _configuration = configuration;
        }

        /// GET: api/v1/ServiceOrderItem/GetAll
        /// <summary>
        /// Endpoint to get all serviceOrderItems
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll(ServiceOrderItemParams pageParams) => Ok(await _serviceOrderItemService.GetAll(pageParams, _configuration["ConnectionStrings:NexusVpcApi"]));

        /// POST: api/v1/ServiceOrderItem/GetById
        /// <summary>
        /// Endpoint to get serviceOrderItem by id
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetById")]
        public async Task<IActionResult> GetById(GetById obj) => Ok(await _serviceOrderItemService.GetById(obj, _configuration["ConnectionStrings:NexusVpcApi"]));


        /// POST: api/v1/ServiceOrderItem/UpdateChassisItems
        /// <summary>
        /// Endpoint to change chassis in serviceOrderItem
        /// </summary>
        /// <returns></returns>
        [HttpPost("UpdateChassisItems")]
        public async Task<IActionResult> UpdateChassisItems(ServiceOrderItemPutDto obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Root.Errors);

            var response = await _serviceOrderItemService.UpdateChassisItems(obj, _configuration["ConnectionStrings:NexusVpcApi"]);
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
