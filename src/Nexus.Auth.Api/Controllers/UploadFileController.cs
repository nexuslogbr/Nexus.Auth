using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexus.Auth.Domain.Enums;
using Nexus.Auth.Repository.Dtos.UploadFile;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Services.Interfaces;
using Nexus.Auth.Repository.Dtos.Vehicle;

namespace Nexus.Auth.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class UploadFileController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUploadFileService _uploadFileService;
        private readonly IVehicleService _vehicleService;
        private readonly IFileVpcService _fileVpcService;

        public UploadFileController(IUploadFileService uploadFileService, IVehicleService vehicleInfoService, IConfiguration configuration, IFileVpcService fileVpcService)
        {
            _uploadFileService = uploadFileService;
            _vehicleService = vehicleInfoService;
            _configuration = configuration;
            _fileVpcService = fileVpcService;
        }

        /// GET: api/v1/UploadFile/GetAll
        /// <summary>
        /// Endpoint to get all uploadFiles
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll(PageParams pageParams) => Ok(await _uploadFileService.GetAll(pageParams, _configuration["ConnectionStrings:NexusUploadApi"]));

        /// POST: api/v1/UploadFile/GetById
        /// <summary>
        /// Endpoint to get uploadFile by id
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetById")]
        public async Task<IActionResult> GetById(GetById obj) => Ok(await _uploadFileService.GetById(obj, _configuration["ConnectionStrings:NexusUploadApi"]));

        /// POST: api/v1/UploadFile/Remove
        /// <summary>
        /// Endpoint to remove an uploadFile
        /// </summary>
        /// <returns></returns>
        [HttpPost("Remove")]
        public async Task<IActionResult> Remove(GetById obj)
        {
            var response = await _uploadFileService.Delete(obj, _configuration["ConnectionStrings:NexusUploadApi"]);
            return response.Success ? Ok(response) : NotFound(response);
        }

        /// POST: api/v1/UploadFile/Post
        /// <summary>
        /// Endpoint to create new uploadFile
        /// </summary>
        /// <returns></returns>
        [HttpPost("Post")]
        public async Task<IActionResult> Post([FromForm] UploadFileToSendDto obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Root.Errors);

            var file = _uploadFileService.FilterFileData(obj.File, obj.Type);

            var response = await _uploadFileService.Post(file, _configuration["ConnectionStrings:NexusUploadApi"]);
            if (!response.Success)
                return BadRequest(response);
            else
            {
                foreach (var order in file.OrderService)
                    order.UploadFileId = response.Data.Id;

                var responseFileVpc = await _fileVpcService.PostRange(file.OrderService, _configuration["ConnectionStrings:NexusUploadApi"]);
                if (!responseFileVpc.Success)
                    return BadRequest(responseFileVpc);
            }

            if (obj.Type == UploadTypeEnum.Chassis)
            {
                var vehicles = new List<VehicleDto>();

                foreach (var os in file.OrderService)
                    if (os.Success)
                        vehicles.Add(new VehicleDto { Chassis = os.Chassis, FileId = response.Data.Id, PlaceId = os.PlaceId, ModelId = os.ModelId });    

                var vehicleResponse = await _vehicleService.PostRange(vehicles, _configuration["ConnectionStrings:NexusVehicleApi"]);

                if (!vehicleResponse.Success)
                    return BadRequest(vehicleResponse);
            }


            return Ok(new UploadFileDisplayDto { FileName = response.Data.Name, ConcludedRegisters = file.ConcludedRegisters, FailedRegisters = file.FailedRegisters });
        }
    }
}
