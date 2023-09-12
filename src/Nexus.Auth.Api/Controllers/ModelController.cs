using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos;
using Nexus.Auth.Repository.Services;
using Nexus.Auth.Repository.Services.Interfaces;

namespace Nexus.Auth.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class ModelController : ControllerBase
    {
        private readonly IModelService _modelService;
        private readonly IConfiguration _configuration;

        public ModelController(IModelService modelService, IConfiguration configuration)
        {
            _modelService = modelService;
            _configuration = configuration;
        }

        /// GET: api/v1/Model/GetAll
        /// <summary>
        /// Endpoint to get all models
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll(PageParams pageParams) => Ok(await _modelService.GetAll(pageParams, _configuration["ConnectionStrings:NexusCustomerApi"]));

        /// POST: api/v1/Model/GetById
        /// <summary>
        /// Endpoint to get model by id
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetById")]
        public async Task<IActionResult> GetById(GetById obj) => Ok(await _modelService.GetById(obj, _configuration["ConnectionStrings:NexusCustomerApi"]));

        /// POST: api/v1/Model/Post
        /// <summary>
        /// Endpoint to create new model
        /// </summary>
        /// <returns></returns>
        [HttpPost("Post")]
        public async Task<IActionResult> Post(ModelDto obj) => Ok(await _modelService.Post(obj, _configuration["ConnectionStrings:NexusCustomerApi"]));

        /// POST: api/v1/Model/Put
        /// <summary>
        /// Endpoint to change an model
        /// </summary>
        /// <returns></returns>
        [HttpPost("Put")]
        public async Task<IActionResult> Put(ModelDto obj) => Ok(await _modelService.Put(obj, _configuration["ConnectionStrings:NexusCustomerApi"]));

        /// POST: api/v1/Model/Remove
        /// <summary>
        /// Endpoint to remove an model
        /// </summary>
        /// <returns></returns>
        [HttpPost("Remove")]
        public async Task<IActionResult> Remove(GetById obj) => Ok(await _modelService.Delete(obj, _configuration["ConnectionStrings:NexusCustomerApi"]));


    }
}
