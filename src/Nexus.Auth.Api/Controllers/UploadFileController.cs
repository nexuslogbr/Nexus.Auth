using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nexus.Auth.Domain.Enums;
using Nexus.Auth.Repository.Dtos.UploadFile;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.UploadFile;
using Nexus.Auth.Repository.Services;
using Nexus.Auth.Repository.Services.Interfaces;
using Nexus.Auth.Repository.Dtos.VehicleInfo;

namespace Nexus.Auth.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class UploadFileController : ControllerBase
    {
        private readonly IUploadFileService _uploadFileService;
        private readonly IVehicleInfoService _vehicleInfoService;
        private readonly IConfiguration _configuration;

        public UploadFileController(IUploadFileService uploadFileService, IVehicleInfoService vehicleInfoService, IConfiguration configuration)
        {
            _uploadFileService = uploadFileService;
            _vehicleInfoService = vehicleInfoService;
            _configuration = configuration;
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
        public async Task<IActionResult> Post([FromForm] UploadFileDto obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Root.Errors);

            var datas = _uploadFileService.GetFileData(obj.File, obj.Type);

            var response = await _uploadFileService.Post(obj, _configuration["ConnectionStrings:NexusUploadApi"]);
            if (!response.Success)
                return BadRequest(response);

            var registerResult = new UploadFileRegisterResultDto();
            if (obj.Type == UploadTypeEnum.Chassis)
            {
                var vehicleResponse = await _vehicleInfoService.PostRange(response.Data.Data, _configuration["ConnectionStrings:NexusVehicleApi"]);
                
                if (!vehicleResponse.Success)
                    return BadRequest(vehicleResponse);

                registerResult = vehicleResponse.Data;

                var changeStatusResponse = await _uploadFileService.ChangeInfo(new UploadFileChangeInfoDto
                {
                    FileId = response.Data.Id,
                    Status = vehicleResponse.Data.Status,
                    ConcludedRegisters = vehicleResponse.Data.ConcludedRegisters,
                    FailedRegisters = vehicleResponse.Data.FailedRegisters
                }, _configuration["ConnectionStrings:NexusUploadApi"]);

                if (!changeStatusResponse.Success)
                    return BadRequest(vehicleResponse);
            }

            return Ok(registerResult);
        }
    }
}
