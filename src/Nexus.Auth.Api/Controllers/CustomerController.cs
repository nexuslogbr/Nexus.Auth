using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexus.Auth.Repository.Dtos.Customer;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Interfaces;

namespace Nexus.Api.Web.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class CustomerController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ICustomerService _customerService;

        public CustomerController(IConfiguration configuration, ICustomerService customerService)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        }


        /// GET: api/v1/Customer/GetAll
        /// <summary>
        /// Endpoint to get all customers
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll(PageParams pageParams) => Ok(await _customerService.GetAll(pageParams, _configuration["ConnectionStrings:NexusCustomerApi"]));

        /// POST: api/v1/Customer/GetById
        /// <summary>
        /// Endpoint to get customer by id
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetById")]
        public async Task<IActionResult> GetById(GetById obj) => Ok(await _customerService.GetById(obj, _configuration["ConnectionStrings:NexusCustomerApi"]));

        /// POST: api/v1/Customer/Post
        /// <summary>
        /// Endpoint to create new customer
        /// </summary>
        /// <returns></returns>
        [HttpPost("Post")]
        public async Task<IActionResult> Post(CustomerDto obj) => Ok(await _customerService.Post(obj, _configuration["ConnectionStrings:NexusCustomerApi"]));

        /// POST: api/v1/Customer/Put
        /// <summary>
        /// Endpoint to change an customer
        /// </summary>
        /// <returns></returns>
        [HttpPost("Put")]
        public async Task<IActionResult> Put(CustomerIdDto obj) => Ok(await _customerService.Put(obj, _configuration["ConnectionStrings:NexusCustomerApi"]));

        /// POST: api/v1/Customer/Remove
        /// <summary>
        /// Endpoint to remove an customer
        /// </summary>
        /// <returns></returns>
        [HttpPost("Remove")]
        public async Task<IActionResult> Remove(GetById obj) => Ok(await _customerService.Delete(obj, _configuration["ConnectionStrings:NexusCustomerApi"]));

        /// POST: api/v1/Customer/ChangeStatus
        /// <summary>
        /// Endpoint to change the status for customer
        /// </summary>
        /// <returns></returns>
        [HttpPost("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus(ChangeStatusDto obj)
        {
            var response = await _customerService.ChangeStatus(obj, _configuration["ConnectionStrings:NexusCustomerApi"]);
            return response.Success ? Ok(response) : NotFound(response);
        }
    }
}
