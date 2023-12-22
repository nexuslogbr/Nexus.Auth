using Microsoft.AspNetCore.Http;
using Nexus.Auth.Repository.Dtos.Auth;
using Nexus.Auth.Repository.Dtos.Place;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Services.Interfaces;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Services
{
    public class PlaceService : IPlaceService
    {
        private readonly IAccessDataService _accessDataService;

        public PlaceService(IAccessDataService accessDataService)
        {
            _accessDataService = accessDataService;
        }

        public async Task<GenericCommandResult<PageList<PlaceResponseDto>>> GetAll(PageParams pageParams, string path)
        {
            var result = await _accessDataService.PostDataAsync<PageList<PlaceResponseDto>>(path, "api/v1/Place/GetAll", pageParams);
            if (result is not null)
                return new GenericCommandResult<PageList<PlaceResponseDto>>(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult<PageList<PlaceResponseDto>>(true, "Error", default, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult<PlaceResponseDto>> GetById(GetById obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<PlaceResponseDto>(path, "api/v1/Place/GetById", obj);
            if (result is not null)
                return new GenericCommandResult<PlaceResponseDto>(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult<PlaceResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult<PlaceResponseDto>> Post(PlaceDto obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<PlaceResponseDto>(path, "api/v1/Place/Post", obj);
            if (result is not null)
                return new GenericCommandResult<PlaceResponseDto>(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult<PlaceResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult<PlaceResponseDto>> Put(PlaceDto obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<PlaceResponseDto>(path, "api/v1/Place/Put", obj);
            if (result is not null)
                return new GenericCommandResult<PlaceResponseDto>(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult<PlaceResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult<ChangeStatusDto>> ChangeStatus(ChangeStatusDto obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<ChangeStatusDto>(path, "api/v1/Place/ChangeStatus", obj);
            if (result is not null)
                return new GenericCommandResult<ChangeStatusDto>(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult<ChangeStatusDto>(true, "Error", default, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult<TokenDto>> Delete(GetById obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<TokenDto>(path, "api/v1/Place/Delete", obj);
            if (result is not null)
                return new GenericCommandResult<TokenDto>(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult<TokenDto>(true, "Error", default, StatusCodes.Status400BadRequest);
        }
    }
}
