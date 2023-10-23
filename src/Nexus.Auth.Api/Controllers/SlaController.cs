using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.Sla;
using Nexus.Auth.Repository.Interfaces;
using Nexus.Auth.Repository.Services.Interfaces;

namespace Nexus.Auth.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class SlaController : ControllerBase
    {
        private readonly ISlaService _slaService;
        private readonly ICustomerService _customerService;
        private readonly IConfiguration _configuration;

        public SlaController(
            ISlaService slaService,
            IConfiguration configuration,
            ICustomerService customerService)
        {
            _slaService = slaService;
            _configuration = configuration;
            _customerService = customerService;
        }

        /// GET: api/v1/Sla/GetAll
        /// <summary>
        /// Endpoint to get all slas
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll(PageParams pageParams) => Ok(await _slaService.GetAll(pageParams, _configuration["ConnectionStrings:NexusVpcApi"]));

        /// POST: api/v1/Sla/GetById
        /// <summary>
        /// Endpoint to get sla by id
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetById")]
        public async Task<IActionResult> GetById(GetById obj) => Ok(await _slaService.GetById(obj, _configuration["ConnectionStrings:NexusVpcApi"]));

        /// POST: api/v1/Sla/Post
        /// <summary>
        /// Endpoint to create new sla
        /// </summary>
        /// <returns></returns>
        [HttpPost("Post")]
        public async Task<IActionResult> Post(SlaDto obj)
        {
            var customer = await _customerService.GetById(
                new GetById { Id = obj.CustomerId },
                _configuration["ConnectionStrings:NexusCustomerApi"]);
            if (!customer.Success) return BadRequest(customer);

            if (!ModelState.IsValid)
                return BadRequest(ModelState.Root.Errors);

            obj.CustomerName = customer.Data.Name;

            var response = await _slaService.Post(obj, _configuration["ConnectionStrings:NexusVpcApi"]);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        /// POST: api/v1/Sla/Put
        /// <summary>
        /// Endpoint to change an sla
        /// </summary>
        /// <returns></returns>
        [HttpPost("Put")]
        public async Task<IActionResult> Put(SlaPutDto obj)
        {
            var customer = await _customerService.GetById(
                new GetById { Id = obj.CustomerId },
                _configuration["ConnectionStrings:NexusCustomerApi"]);
            if (!customer.Success) return BadRequest(customer);

            if (!ModelState.IsValid)
                return BadRequest(ModelState.Root.Errors);

            obj.CustomerName = customer.Data.Name;

            var response = await _slaService.Put(obj, _configuration["ConnectionStrings:NexusVpcApi"]);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        /// POST: api/v1/Sla/Remove
        /// <summary>
        /// Endpoint to remove an sla
        /// </summary>
        /// <returns></returns>
        [HttpPost("Remove")]
        public async Task<IActionResult> Remove(GetById obj)
        {
            var response = await _slaService.Delete(obj, _configuration["ConnectionStrings:NexusVpcApi"]);
            return response.Success ? Ok(response) : NotFound(response);
        }

    }
}
