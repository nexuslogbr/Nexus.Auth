using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexus.Auth.Repository.Dtos.FileVpc;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.UploadFile;
using Nexus.Auth.Repository.Services.Interfaces;

namespace Nexus.Auth.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class FileVpcController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IFileVpcService _fileVpcService;

        public FileVpcController(IConfiguration configuration, IFileVpcService fileVpcService)
        {
            _configuration = configuration;
            _fileVpcService = fileVpcService;
        }


        /// GET: api/v1/FileVpc/GetAll
        /// <summary>
        /// Endpoint to get all models
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll(PageParams pageParams) => Ok(await _fileVpcService.GetAll(pageParams, _configuration["ConnectionStrings:NexusUploadApi"]));

        /// POST: api/v1/FileVpc/GetById
        /// <summary>
        /// Endpoint to get model by id
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetById")]
        public async Task<IActionResult> GetById(GetById obj) => Ok(await _fileVpcService.GetById(obj, _configuration["ConnectionStrings:NexusUploadApi"]));

        /// POST: api/v1/FileVpc/GetByFileId
        /// <summary>
        /// Endpoint to get model by file id
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetByFileId")]
        public async Task<IActionResult> GetByFileId(GetById obj)
        {
            var files = (await _fileVpcService.GetAllByFileId(obj, _configuration["ConnectionStrings:NexusUploadApi"])).Data;

            var res = new UploadFileDisplayDto();
            int i = 1;

            foreach (var os in files)
            {
                res.Data += os.Chassis + "          " + (os.Success ? "OK" : os.Error) + "\n";
                i++;
            }

            return Ok(res);
        }

        /// POST: api/v1/FileVpc/Post
        /// <summary>
        /// Endpoint to create new model
        /// </summary>
        /// <returns></returns>
        [HttpPost("Post")]
        public async Task<IActionResult> Post(FileVpcDto obj) => Ok(await _fileVpcService.Post(obj, _configuration["ConnectionStrings:NexusUploadApi"]));

        /// POST: api/v1/FileVpc/Put
        /// <summary>
        /// Endpoint to change an model
        /// </summary>
        /// <returns></returns>
        [HttpPost("Put")]
        public async Task<IActionResult> Put(FileVpcDto obj) => Ok(await _fileVpcService.Put(obj, _configuration["ConnectionStrings:NexusUploadApi"]));

        /// POST: api/v1/FileVpc/Remove
        /// <summary>
        /// Endpoint to remove an model
        /// </summary>
        /// <returns></returns>
        [HttpPost("Remove")]
        public async Task<IActionResult> Remove(GetById obj) => Ok(await _fileVpcService.Delete(obj, _configuration["ConnectionStrings:NexusUploadApi"]));

        /// POST: api/v1/FileVpc/ChangeStatus
        /// <summary>
        /// Endpoint to change the status for model
        /// </summary>
        /// <returns></returns>
        [HttpPost("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus(ChangeStatusDto obj)
        {
            var response = await _fileVpcService.ChangeStatus(obj, _configuration["ConnectionStrings:NexusUploadApi"]);
            return response.Success ? Ok(response) : NotFound(response);
        }

    }
}
