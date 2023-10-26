using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Models;
using Nexus.Auth.Repository.Services.Interfaces;
using Nexus.Auth.Repository.Dtos.VpcItem;

namespace Nexus.Auth.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class VpcItemController : ControllerBase
    {
        private readonly IVpcItemService _vpcItemService;
        private readonly IModelService _modelService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IConfiguration _configuration;

        public VpcItemController(
            IVpcItemService vpcItemService,
            IModelService modelService,
            IManufacturerService manufacturerService,
            IConfiguration configuration)
        {
            _vpcItemService = vpcItemService;
            _modelService = modelService;
            _manufacturerService = manufacturerService;
            _configuration = configuration;
        }

        /// GET: api/v1/VpcItem/GetAll
        /// <summary>
        /// Endpoint to get all vpcItems
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll(PageParams pageParams) => Ok(await _vpcItemService.GetAll(pageParams, _configuration["ConnectionStrings:NexusVpcApi"]));

        /// POST: api/v1/VpcItem/GetByModelId
        /// <summary>
        /// Endpoint to get vpcItem by model id
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetByModelId")]
        public async Task<IActionResult> GetByModelId(GetById obj) => Ok(await _vpcItemService.GetByModelId(obj, _configuration["ConnectionStrings:NexusVpcApi"]));

        /// POST: api/v1/VpcItem/GetByChassis
        /// <summary>
        /// Endpoint to get vpcItem by chassis number
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetByChassis")]
        public async Task<IActionResult> GetByChassis(GetByName dto)
        {
            var vds = dto.Name.Substring(3, 6);
            var model = await _modelService.GetByVds(new GetByName
            {
                Name = vds
            }, _configuration["ConnectionStrings:NexusVehicleApi"]);
            if (!model.Success || model.Data is null) return BadRequest(model);
            return Ok(await _vpcItemService.GetByModelId(new GetById
            {
                Id = model.Data.Id
            }, _configuration["ConnectionStrings:NexusVpcApi"]));
        }

        /// POST: api/v1/VpcItem/GetById
        /// <summary>
        /// Endpoint to get vpcItem by id
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetById")]
        public async Task<IActionResult> GetById(GetById obj) => Ok(await _vpcItemService.GetById(obj, _configuration["ConnectionStrings:NexusVpcApi"]));
        /// POST: api/v1/VpcItem/Post
        /// <summary>
        /// Endpoint to create new vpcItem
        /// </summary>
        /// <returns></returns>
        [HttpPost("Post")]
        public async Task<IActionResult> Post(VpcItemDto obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Root.Errors);

            var manufacturer = await _manufacturerService.GetById(
                new GetById { Id = obj.ManufacturerId },
                _configuration["ConnectionStrings:NexusVehicleApi"]);
            if (!manufacturer.Success) return BadRequest(manufacturer);

            var modelList = new List<VpcItemModelDto>();
            foreach (var model in obj.Models)
            {
                var currentModel = await _modelService.GetById(
                    new GetById { Id = model.ModelId },
                    _configuration["ConnectionStrings:NexusVehicleApi"]);
                if (!currentModel.Success) return BadRequest(model);
                modelList.Add(new VpcItemModelDto
                {
                    ModelId = model.ModelId,
                    Name = currentModel.Data.Name
                });
            }
            obj.ManufacturerName = manufacturer.Data.Name;
            obj.Models = modelList;

            var response = await _vpcItemService.Post(obj, _configuration["ConnectionStrings:NexusVpcApi"]);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        /// POST: api/v1/VpcItem/Put
        /// <summary>
        /// Endpoint to change an vpcItem
        /// </summary>
        /// <returns></returns>
        [HttpPost("Put")]
        public async Task<IActionResult> Put(VpcItemPutDto obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Root.Errors);

            var manufacturer = await _manufacturerService.GetById(
                new GetById { Id = obj.ManufacturerId },
                _configuration["ConnectionStrings:NexusVehicleApi"]);
            if (!manufacturer.Success) return BadRequest(manufacturer);

            var modelList = new List<VpcItemModelDto>();
            foreach (var model in obj.Models)
            {
                var currentModel = await _modelService.GetById(
                    new GetById { Id = model.ModelId },
                    _configuration["ConnectionStrings:NexusVehicleApi"]);
                if (!currentModel.Success) return BadRequest(model);
                modelList.Add(new VpcItemModelDto
                {
                    ModelId = model.ModelId,
                    Name = currentModel.Data.Name
                });
            }
            obj.ManufacturerName = manufacturer.Data.Name;
            obj.Models = modelList;

            var response = await _vpcItemService.Put(obj, _configuration["ConnectionStrings:NexusVpcApi"]);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        /// POST: api/v1/VpcItem/Remove
        /// <summary>
        /// Endpoint to remove an vpcItem
        /// </summary>
        /// <returns></returns>
        [HttpPost("Remove")]
        public async Task<IActionResult> Remove(GetById obj)
        {
            var response = await _vpcItemService.Delete(obj, _configuration["ConnectionStrings:NexusVpcApi"]);
            return response.Success ? Ok(response) : NotFound(response);
        }
    }
}
