using Microsoft.AspNetCore.Http;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.OrderService;
using Nexus.Auth.Repository.Dtos.UploadFile;
using Nexus.Auth.Repository.Services.Interfaces;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Services
{
    public class FileVpcService : IFileVpcService
    {
        private readonly IAccessDataService _accessDataService;

        public FileVpcService(IAccessDataService accessDataService)
        {
            _accessDataService = accessDataService;
        }

        public async Task<GenericCommandResult<FileVpcResponseDto>> GetByChassi(GetByChassi entity, string path)
        {
            var result = await _accessDataService.PostDataAsync<FileVpcResponseDto>(path, "api/v1/FileVpc/GetByChassis", entity);
            if (result is not null)
                return new GenericCommandResult<FileVpcResponseDto>(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult<FileVpcResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult<object>> PostRange(List<OrderServiceDto> entities, string path)
        {
            var result = await _accessDataService.PostDataAsync<object>(path, "api/v1/FileVpc/PostRange", entities);
            if (result is not null)
                return new GenericCommandResult<object>(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult<object>(true, "Error", default, StatusCodes.Status400BadRequest);
        }
    }
}
