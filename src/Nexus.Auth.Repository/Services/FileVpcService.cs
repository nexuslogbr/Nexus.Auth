using Microsoft.AspNetCore.Http;
using Nexus.Auth.Repository.Dtos.Auth;
using Nexus.Auth.Repository.Dtos.FileVpc;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.Model;
using Nexus.Auth.Repository.Dtos.OrderService;
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

        public async Task<GenericCommandResult<FileVpcResponseDto>> Put(FileVpcDto dto, string path)
        {
            throw new NotImplementedException();
        }

        public async Task<GenericCommandResult<ChangeStatusDto>> ChangeStatus(ChangeStatusDto obj, string path)
        {
            throw new NotImplementedException();
        }

        public async Task<GenericCommandResult<TokenDto>> Delete(GetById obj, string path)
        {
            throw new NotImplementedException();
        }

        public async Task<GenericCommandResult<PageList<FileVpcResponseDto>>> GetAll(PageParams pageParams, string path)
        {
            throw new NotImplementedException();
        }

        public async Task<GenericCommandResult<List<FileVpcResponseDto>>> GetAllByFileId(GetById dto, string path)
        {
            var result = await _accessDataService.PostDataAsync<List<FileVpcResponseDto>>(path, "api/v1/FileVpc/GetByFileId", dto);
            if (result is not null)
                return new GenericCommandResult<List<FileVpcResponseDto>>(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult<List<FileVpcResponseDto>>(true, "Error", default, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult<FileVpcResponseDto>> GetByName(GetByName dto, string path)
        {
            throw new NotImplementedException();
        }

        public async Task<GenericCommandResult<FileVpcResponseDto>> Post(FileVpcDto dto, string path)
        {
            throw new NotImplementedException();
        }

        public Task<GenericCommandResult<FileVpcResponseDto>> GetById(GetById dto, string path)
        {
            throw new NotImplementedException();
        }
    }
}
