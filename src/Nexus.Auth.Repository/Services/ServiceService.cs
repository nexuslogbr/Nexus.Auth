using Microsoft.AspNetCore.Http;
using Nexus.Auth.Repository.Dtos;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Interfaces;
using Nexus.Auth.Repository.Models;
using Nexus.Auth.Repository.Services.Interfaces;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Services
{
    public class ServiceService : IServiceService<ServiceDto>
    {
        private readonly IAccessDataService _accessDataService;

        public ServiceService(IAccessDataService accessDataService)
        {
            _accessDataService = accessDataService ?? throw new ArgumentException(nameof(accessDataService));
        }

        public async Task<GenericCommandResult> GetAll(PageParams pageParams, string path)
        {
            var result = await _accessDataService.PostDataAsync<ServiceModel[]>(path, "/api/v1/Service/GetAll", pageParams);
            if (result is not null)
                return new GenericCommandResult(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult(true, "Error", new object { }, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult> GetById(GetById obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<ServiceDto>(path, "/api/v1/Service/GetById", obj);
            if (result is not null)
                return new GenericCommandResult(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult(true, "Error", new object { }, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult> GetByName(GetByName obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<TokenDto>(path, "/api/v1/Service/GetByName", obj);
            if (result is not null)
                return new GenericCommandResult(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult(true, "Error", new object { }, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult> Post(ServiceDto obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<TokenDto>(path, "/api/v1/Service/Post", obj);
            if (result is not null)
                return new GenericCommandResult(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult(true, "Error", new object { }, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult> Put(ServiceDto obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<TokenDto>(path, "/api/v1/Service/Put", obj);
            if (result is not null)
                return new GenericCommandResult(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult(true, "Error", new object { }, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult> Delete(GetById obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<TokenDto>(path, "/api/v1/Service/Delete", obj);
            if (result is not null)
                return new GenericCommandResult(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult(true, "Error", new object { }, StatusCodes.Status400BadRequest);
        }

    }
}
