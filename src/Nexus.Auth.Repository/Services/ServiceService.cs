using Microsoft.AspNetCore.Http;
using Nexus.Auth.Repository.Dtos;
using Nexus.Auth.Repository.Dtos.Generics;
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
            _accessDataService = accessDataService ?? throw new ArgumentException(nameof(accessDataService));
        }

        public async Task<GenericCommandResult<IEnumerable<ServiceModel>>> GetAll(PageParams pageParams, string path)
        {
            var result = await _accessDataService.PostDataAsync<IEnumerable<ServiceModel>>(path, "/api/v1/Service/GetAll", pageParams);
            if (result is not null)
                return new GenericCommandResult<IEnumerable<ServiceModel>>(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult<IEnumerable<ServiceModel>>(true, "Error", default, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult<ServiceModel>> GetById(GetById obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<ServiceModel>(path, "/api/v1/Service/GetById", obj);
            if (result is not null)
                return new GenericCommandResult<ServiceModel>(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult<ServiceModel>(true, "Error", default, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult<IEnumerable<ServiceModel>>> GetByName(GetByName obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<IEnumerable<ServiceModel>>(path, "/api/v1/Service/GetByName", obj);
            if (result is not null)
                return new GenericCommandResult<IEnumerable<ServiceModel>>(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult<IEnumerable<ServiceModel>>(true, "Error", default, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult<ServiceModel>> Post(ServiceDto obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<ServiceModel>(path, "/api/v1/Service/Post", obj);
            if (result is not null)
                return new GenericCommandResult<ServiceModel>(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult<ServiceModel>(true, "Error", default, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult<ServiceModel>> Put(ServiceDto obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<ServiceModel>(path, "/api/v1/Service/Put", obj);
            if (result is not null)
                return new GenericCommandResult<ServiceModel>(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult<ServiceModel>(true, "Error", default, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult<TokenDto>> Delete(GetById obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<TokenDto>(path, "/api/v1/Service/Delete", obj);
            if (result is not null)
                return new GenericCommandResult<TokenDto>(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult<TokenDto>(true, "Error", default, StatusCodes.Status400BadRequest);
        }

    }
}
