using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.Model;
using Nexus.Auth.Repository.Dtos.OrderService;
using Nexus.Auth.Repository.Services.Interfaces;

namespace Nexus.Auth.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderServiceController : ControllerBase
    {
        private readonly IOrderServiceService _orderServiceService;
        private readonly IConfiguration _configuration;

        public OrderServiceController(IOrderServiceService orderServiceService, IConfiguration configuration)
        {
            _orderServiceService = orderServiceService;
            _configuration = configuration;
        }

        /// GET: api/v1/OrderService/GetAll
        /// <summary>
        /// Endpoint to get all order service
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll(PageParams pageParams) => Ok(await _orderServiceService.GetAll(pageParams, _configuration["ConnectionStrings:NexusVpcApi"]));

        /// POST: api/v1/OrderService/GetServiceByIdAsync
        /// <summary>
        /// Endpoint to get service of order by id
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetServiceById")]
        public async Task<IActionResult> GetServiceByIdAsync(GetById obj) => Ok(await _orderServiceService.GetServiceByIdAsync(obj, _configuration["ConnectionStrings:NexusVpcApi"]));

        /// POST: api/v1/OrderService/GetById
        /// <summary>
        /// Endpoint to get order service by id
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetById")]
        public async Task<IActionResult> GetById(GetById obj) => Ok(await _orderServiceService.GetById(obj, _configuration["ConnectionStrings:NexusVpcApi"]));

        /// POST: api/v1/OrderService/Post
        /// <summary>
        /// Endpoint to create new order service
        /// </summary>
        /// <returns></returns>
        [HttpPost("Post")]
        public async Task<IActionResult> Post(OrderServiceToSaveDto obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Root.Errors);

            var response = await _orderServiceService.Post(obj, _configuration["ConnectionStrings:NexusVpcApi"]);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        /// POST: api/v1/OrderService/Put
        /// <summary>
        /// Endpoint to change an order service
        /// </summary>
        /// <returns></returns>
        [HttpPost("Put")]
        public async Task<IActionResult> Put(OrderServiceToSavePutDto obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Root.Errors);

            var response = await _orderServiceService.Put(obj, _configuration["ConnectionStrings:NexusVpcApi"]);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        /// POST: api/v1/OrderService/Remove
        /// <summary>
        /// Endpoint to remove an order service
        /// </summary>
        /// <returns></returns>
        [HttpPost("Remove")]
        public async Task<IActionResult> Remove(GetById obj)
        {
            var response = await _orderServiceService.Delete(obj, _configuration["ConnectionStrings:NexusVpcApi"]);
            return response.Success ? Ok(response) : NotFound(response);
        }

        /// POST: api/v1/OrderService/RemoveServicesRange
        /// <summary>
        /// Endpoint to remove services of order service
        /// </summary>
        /// <returns></returns>
        [HttpPost("RemoveServicesRange")]
        public async Task<IActionResult> RemoveServicesRange(OrderServiceRemoveServicesDto dto)
        {
            try
            {
                var result = await _orderServiceService.RemoveServicesRange(dto, _configuration["ConnectionStrings:NexusVpcApi"]);
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex + " Query error");
            }
        }

        /// POST: api/v1/OrderService/ChangeStatus
        /// <summary>
        /// Endpoint to change the status for order service
        /// </summary>
        /// <returns></returns>
        [HttpPost("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus(ChangeStatusDto obj)
        {
            var response = await _orderServiceService.ChangeStatus(obj, _configuration["ConnectionStrings:NexusVpcApi"]);
            return response.Success ? Ok(response) : NotFound(response);
        }

        /// POST: api/v1/OrderService/Filter
        /// <summary>
        /// Endpoint to search data to filters
        /// </summary>
        /// <returns></returns>
        [HttpPost("Filter")]
        public async Task<IActionResult> Filter(OrderServiceFilter filter) => Ok(await _orderServiceService.Filter(filter, _configuration["ConnectionStrings:NexusVpcApi"]));

        /// POST: api/v1/OrderService/CreateList
        /// <summary>
        /// Endpoint to create a list
        /// </summary>
        /// <returns></returns>
        [HttpPost("CreateList")]
        public async Task<IActionResult> CreateList(List<int> obj) => Ok(await _orderServiceService.CreateList(obj, _configuration["ConnectionStrings:NexusVpcApi"]));

        /// POST: api/v1/OrderService/FilterLists
        /// <summary>
        /// Endpoint to filter the lists
        /// </summary>
        /// <returns></returns>
        [HttpPost("FilterLists")]
        public async Task<IActionResult> FilterLists(OrderServiceListFilter obj) => Ok(await _orderServiceService.FilterLists(obj, _configuration["ConnectionStrings:NexusVpcApi"]));

        /// POST: api/v1/OrderService/GetListById
        /// <summary>
        /// Endpoint to get a list by id
        /// <returns></returns>
        [HttpPost("GetListById")]
        public async Task<IActionResult> GetListById(GetById obj) => Ok(await _orderServiceService.GetListById(obj, _configuration["ConnectionStrings:NexusVpcApi"]));

        /// POST: api/v1/OrderService/GetUniqueStreetsAsync
        /// <summary>
        /// Endpoint to get a streets by services id
        /// <returns></returns>
        [HttpPost("GetUniqueStreetsAsync")]
        public async Task<IActionResult> GetUniqueStreetsAsync(GetByIdsDto obj) => Ok(await _orderServiceService.GetUniqueStreetsAsync(obj, _configuration["ConnectionStrings:NexusVpcApi"]));

        /// POST: api/v1/OrderService/GetOrdersbyStreetAsync
        /// <summary>
        /// Endpoint to get orders by streets and services
        /// <returns></returns>
        [HttpPost("GetOrdersbyStreetAsync")]
        public async Task<IActionResult> GetOrdersbyStreetAsync(OrderServiceByStreetDto obj) => Ok(await _orderServiceService.GetOrdersbyStreetAsync(obj, _configuration["ConnectionStrings:NexusVpcApi"]));
    }
}
