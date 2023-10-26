using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.ServiceOrder;
using Nexus.Auth.Repository.Interfaces;
using Nexus.Auth.Repository.Params;
using Nexus.Auth.Repository.Services;
using Nexus.Auth.Repository.Services.Interfaces;

namespace Nexus.Auth.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class ServiceOrderController : ControllerBase
    {

        private readonly IServiceOrderService _serviceOrderService;
        private readonly IVpcItemService _vpcItemService;
        private readonly IChassisService _chassisService;
        private readonly ICustomerService _customerService;
        private readonly IModelService _modelService;
        private readonly IConfiguration _configuration;

        public ServiceOrderController(
            IServiceOrderService serviceOrderService, 
            IConfiguration configuration, 
            ICustomerService customerService, 
            IChassisService chassisService, IModelService modelService, IVpcItemService vpcItemService)
        {
            _serviceOrderService = serviceOrderService;
            _configuration = configuration;
            _customerService = customerService;
            _chassisService = chassisService;
            _modelService = modelService;
            _vpcItemService = vpcItemService;
        }

        /// GET: api/v1/ServiceOrder/GetAll
        /// <summary>
        /// Endpoint to get all serviceOrders
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll(PageParams pageParams) => Ok(await _serviceOrderService.GetAll(pageParams, _configuration["ConnectionStrings:NexusVpcApi"]));

        /// POST: api/v1/ServiceOrder/GetById
        /// <summary>
        /// Endpoint to get serviceOrder by id
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetById")]
        public async Task<IActionResult> GetById(GetById obj) => Ok(await _serviceOrderService.GetById(obj, _configuration["ConnectionStrings:NexusVpcApi"]));

        /// POST: api/v1/ServiceOrder/Post
        /// <summary>
        /// Endpoint to create new serviceOrder
        /// </summary>
        /// <returns></returns>
        [HttpPost("Post")]
        public async Task<IActionResult> Post(ServiceOrderDto obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Root.Errors);
            
            var customer = await _customerService.GetById(
                new GetById { Id = obj.CustomerId },
                _configuration["ConnectionStrings:NexusCustomerApi"]);
            if (!customer.Success || customer.Data is null) return BadRequest("Cliente não foi encontrado.");
            obj.CustomerName = customer.Data.Name;

            var chassisInfos = await _chassisService.GetChassisInfo(new ChassisInfoParams
            {
                ChassisIds = obj.OrderItems.Select(x => x.ChassisId)
            }, _configuration["ConnectionStrings:NexusVehicleApi"]);
            if (!chassisInfos.Success || chassisInfos.Data is null) return BadRequest("Chassis não encontrados.");

            foreach (var orderItem in obj.OrderItems)
            {
                var info = chassisInfos.Data.First(x => x.Id == orderItem.ChassisId);
                if (info is null) return BadRequest("Chassi não foi encontrado.");
                orderItem.ChassisNumber = info.ChassisNumber;
                orderItem.ModelId = info.ModelId;
            }
            
            var response = await _serviceOrderService.Post(obj, _configuration["ConnectionStrings:NexusVpcApi"]);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        /// POST: api/v1/ServiceOrder/Put
        /// <summary>
        /// Endpoint to change an serviceOrder
        /// </summary>
        /// <returns></returns>
        [HttpPost("Put")]
        public async Task<IActionResult> Put(ServiceOrderPutDto obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Root.Errors);

            var response = await _serviceOrderService.Put(obj, _configuration["ConnectionStrings:NexusVpcApi"]);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        /// POST: api/v1/ServiceOrder/Remove
        /// <summary>
        /// Endpoint to remove an serviceOrder
        /// </summary>
        /// <returns></returns>
        [HttpPost("Remove")]
        public async Task<IActionResult> Remove(GetById obj)
        {
            var response = await _serviceOrderService.Delete(obj, _configuration["ConnectionStrings:NexusVpcApi"]);
            return response.Success ? Ok(response) : NotFound(response);
        }
    }
}
