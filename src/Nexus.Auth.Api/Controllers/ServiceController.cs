using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexus.Auth.Repository.Dtos.Customer;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.Service;
using Nexus.Auth.Repository.Dtos.VpcItem;
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
        private readonly IServiceService _serviceService;
        private readonly ICustomerService _customerService;

        public ServiceController(IConfiguration configuration, IServiceService serviceService, ICustomerService customerService)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _serviceService = serviceService ?? throw new ArgumentNullException(nameof(serviceService));
            _customerService = customerService;
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

        /// POST: api/v1/ServiceType/Post
        /// <summary>
        /// Endpoint to create new serviceType
        /// </summary>
        /// <returns></returns>
        [HttpPost("Post")]
        public async Task<IActionResult> Post(ServiceDto obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Root.Errors);

            var customers = await GetCustomerNames(obj.ServiceCustomers);
            if (!customers.Any()) return BadRequest("Cliente não encontrado.");

            var response = await _serviceService.Post(obj, _configuration["ConnectionStrings:NexusVpcApi"]);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        /// POST: api/v1/Service/Put
        /// <summary>
        /// Endpoint to change an service
        /// </summary>
        /// <returns></returns>
        [HttpPost("Put")]
        public async Task<IActionResult> Put(ServicePutDto obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Root.Errors);

            var customers = await GetCustomerNames(obj.ServiceCustomers);
            if (!customers.Any()) return BadRequest("Cliente não encontrado.");

            var response = await _serviceService.Put(obj, _configuration["ConnectionStrings:NexusVpcApi"]);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        /// POST: api/v1/Service/Remove
        /// <summary>
        /// Endpoint to remove an service
        /// </summary>
        /// <returns></returns>
        [HttpPost("Remove")]
        public async Task<IActionResult> Remove(GetById obj) => Ok(await _serviceService.Delete(obj, _configuration["ConnectionStrings:NexusVpcApi"]));

        private async Task<List<CustomerVpcDto>> GetCustomerNames(IList<CustomerVpcDto> customers)
        {
            var list = new List<CustomerVpcDto>();
            foreach (var customer in customers)
            {
                var currentModel = await _customerService.GetById(new GetById { Id = customer.CustomerId }, _configuration["ConnectionStrings:NexusCustomerApi"]);
                if (!currentModel.Success) return null;
                customer.CustomerId = currentModel.Data.Id;
                list.Add(customer);
            }
            return list;
        }

        /// POST: api/v1/Service/ChangeStatus
        /// <summary>
        /// Endpoint to change the status for service
        /// </summary>
        /// <returns></returns>
        [HttpPost("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus(ChangeStatusDto obj)
        {
            var response = await _serviceService.ChangeStatus(obj, _configuration["ConnectionStrings:NexusVpcApi"]);
            return response.Success ? Ok(response) : NotFound(response);
        }
    }
}
