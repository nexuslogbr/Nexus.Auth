using Microsoft.AspNetCore.Http;
using Nexus.Auth.Repository.Dtos.Auth;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.Service;
using Nexus.Auth.Repository.Dtos.Service;
using Nexus.Auth.Repository.Interfaces;
using Nexus.Auth.Repository.Models;
using Nexus.Auth.Repository.Services.Interfaces;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IAccessDataService _accessDataService;

        public ServiceService(IAccessDataService accessDataService)
        {
            _accessDataService = accessDataService;
        }

        public async Task<GenericCommandResult<PageList<ServiceResponseDto>>> GetAll(PageParams pageParams, string path)
        {
            var result = await _accessDataService.PostDataAsync<PageList<ServiceResponseDto>>(path, "api/v1/Service/GetAll", pageParams);
            if (result is not null)
                return new GenericCommandResult<PageList<ServiceResponseDto>>(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult<PageList<ServiceResponseDto>>(true, "Error", default, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult<ServiceResponseDto>> GetById(GetById obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<ServiceResponseDto>(path, "api/v1/Service/GetById", obj);
            if (result is not null)
                return new GenericCommandResult<ServiceResponseDto>(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult<ServiceResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult<ChangeStatusDto>> ChangeStatus(ChangeStatusDto obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<ChangeStatusDto>(path, "api/v1/Service/ChangeStatus", obj);
            if (result is not null)
                return new GenericCommandResult<ChangeStatusDto>(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult<ChangeStatusDto>(true, "Error", default, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult<ServiceResponseDto>> Post(ServiceDto obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<ServiceResponseDto>(path, "api/v1/Service/Post", obj);
            if (result is not null)
                return new GenericCommandResult<ServiceResponseDto>(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult<ServiceResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult<ServiceResponseDto>> Put(ServiceDto obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<ServiceResponseDto>(path, "api/v1/Service/Put", obj);
            if (result is not null)
                return new GenericCommandResult<ServiceResponseDto>(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult<ServiceResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult<TokenDto>> Delete(GetById obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<TokenDto>(path, "api/v1/Service/Delete", obj);
            if (result is not null)
                return new GenericCommandResult<TokenDto>(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult<TokenDto>(true, "Error", default, StatusCodes.Status400BadRequest);
        }
    }
}
