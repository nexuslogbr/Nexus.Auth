using Microsoft.AspNetCore.Http;
using Nexus.Auth.Repository.Dtos;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Models;
using Nexus.Auth.Repository.Services.Interfaces;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Services
{
    public class ManufacturerService : IManufacturerService<ManufacturerDto>
    {
        private readonly IAccessDataService _accessDataService;

        public ManufacturerService(IAccessDataService accessDataService)
        {
            _accessDataService = accessDataService;
        }
        public async Task<GenericCommandResult> GetAll(PageParams pageParams, string path)
        {
            var result = await _accessDataService.PostDataAsync<ManufacturerModel[]>(path, "api/v1/Manufacturer/GetAll", pageParams);
            if (result is not null)
                return new GenericCommandResult(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult(true, "Error", new object { }, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult> GetById(GetById obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<ManufacturerModel>(path, "api/v1/Manufacturer/GetById", obj);
            if (result is not null)
                return new GenericCommandResult(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult(true, "Error", new object { }, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult> GetByName(GetByName obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<ManufacturerModel>(path, "api/v1/Manufacturer/GetByName", obj);
            if (result is not null)
                return new GenericCommandResult(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult(true, "Error", new object { }, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult> Post(ManufacturerDto obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<ManufacturerModel>(path, "api/v1/Manufacturer/Post", obj);
            if (result is not null)
                return new GenericCommandResult(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult(true, "Error", new object { }, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult> Put(ManufacturerDto obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<ManufacturerModel>(path, "api/v1/Manufacturer/Put", obj);
            if (result is not null)
                return new GenericCommandResult(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult(true, "Error", new object { }, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult> Delete(GetById obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<TokenDto>(path, "api/v1/Manufacturer/Delete", obj);
            if (result is not null)
                return new GenericCommandResult(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult(true, "Error", new object { }, StatusCodes.Status400BadRequest);
        }

    }
}
