using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos;
using Nexus.Auth.Repository.Interfaces;
using Nexus.Auth.Repository.Services.Interfaces;

namespace Nexus.Auth.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class VpcStorageController : ControllerBase
    {
        private readonly IVpcStorageService _vpcStorageService;
        private readonly ICustomerService _customerService;
        private readonly IConfiguration _configuration;

        public VpcStorageController(
            IVpcStorageService vpcStorageService, 
            IConfiguration configuration, 
            ICustomerService customerService)
        {
            _vpcStorageService = vpcStorageService;
            _configuration = configuration;
            _customerService = customerService;
        }

        /// GET: api/v1/VpcStorage/GetAll
        /// <summary>
        /// Endpoint to get all vpcStorages
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll(PageParams pageParams) => Ok(await _vpcStorageService.GetAllFlat(pageParams, _configuration["ConnectionStrings:NexusVpcApi"]));

        /// POST: api/v1/VpcStorage/GetById
        /// <summary>
        /// Endpoint to get vpcStorage by id
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetById")]
        public async Task<IActionResult> GetById(GetById obj) => Ok(await _vpcStorageService.GetById(obj, _configuration["ConnectionStrings:NexusVpcApi"]));

        /// POST: api/v1/VpcStorage/GetById
        /// <summary>
        /// Endpoint to get vpcStorages by name
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetByName")]
        public async Task<IActionResult> GetByName(GetByName obj) => Ok(await _vpcStorageService.GetByName(obj, _configuration["ConnectionStrings:NexusVpcApi"]));

        /// POST: api/v1/VpcStorage/Post
        /// <summary>
        /// Endpoint to create new vpcStorage
        /// </summary>
        /// <returns></returns>
        [HttpPost("Post")]
        public async Task<IActionResult> Post(VpcStorageDto obj)
        {
            var customer = await _customerService.GetById(
                new GetById { Id = obj.CustomerId },
                _configuration["ConnectionStrings:NexusCustomerApi"]);
            if (!customer.Success) return BadRequest(customer);

            if (!ModelState.IsValid)
                return BadRequest(ModelState.Root.Errors);

            obj.CustomerName = customer.Data.Name;

            var response = await _vpcStorageService.Post(obj, _configuration["ConnectionStrings:NexusVpcApi"]);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        /// POST: api/v1/VpcStorage/Put
        /// <summary>
        /// Endpoint to change an vpcStorage
        /// </summary>
        /// <returns></returns>
        [HttpPost("Put")]
        public async Task<IActionResult> Put(VpcStoragePutDto obj)
        {
            var customer = await _customerService.GetById(
                new GetById { Id = obj.CustomerId },
                _configuration["ConnectionStrings:NexusCustomerApi"]);
            if (!customer.Success) return BadRequest(customer);

            if (!ModelState.IsValid)
                return BadRequest(ModelState.Root.Errors);

            obj.CustomerName = customer.Data.Name;

            var response = await _vpcStorageService.Put(obj, _configuration["ConnectionStrings:NexusVpcApi"]);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        /// POST: api/v1/VpcStorage/Remove
        /// <summary>
        /// Endpoint to remove an vpcStorage
        /// </summary>
        /// <returns></returns>
        [HttpPost("Remove")]
        public async Task<IActionResult> Remove(GetById obj)
        {
            var response = await _vpcStorageService.Delete(obj, _configuration["ConnectionStrings:NexusVpcApi"]);
            return response.Success ? Ok(response) : NotFound(response);
        }
    }
}
