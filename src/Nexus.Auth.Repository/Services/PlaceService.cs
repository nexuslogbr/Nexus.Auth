using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.Place;
using Nexus.Auth.Repository.Services.Interfaces;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Services
{
    public class PlaceService : IPlaceService
    {
        private readonly IAccessDataService _accessDataService;
        private readonly string _url;

        public PlaceService(IAccessDataService accessDataService, IConfiguration config)
        {
            _accessDataService = accessDataService;
            _url = config.GetValue<string>("ConnectionStrings:NexusCustomerApi") ?? throw new InvalidOperationException("can not get config \"ConnectionStrings:NexusCustomerApi\"");
        }


        public async Task<GenericCommandResult<PlaceResponseDto>> GetById(int id)
        {
            var result = await _accessDataService.PostDataAsync<PlaceResponseDto>(_url, "Place/GetById", new GetById { Id = id });
            if (result is not null)
                return new GenericCommandResult<PlaceResponseDto>(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult<PlaceResponseDto>(true, "Error", null, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult<List<PlaceResponseDto>>> GetByIds(List<int> dto)
        {
            var result = await _accessDataService.PostDataAsync<List<PlaceResponseDto>>(_url, "Place/GetByIds", dto);
            if (result is not null)
                return new GenericCommandResult<List<PlaceResponseDto>>(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult<List<PlaceResponseDto>>(true, "Error", null, StatusCodes.Status400BadRequest);
        }


        //public async Task<GenericCommandResult<List<PlaceResponseDto>>> GetByIds(List<int> dto)
        //{
        //    var result = await _accessDataService.PostDataAsync<GenericCommandResult<List<PlaceResponseDto>>>(_url, "Place/GetByIds", dto);
        //    if (result is not null)
        //        return result;

        //    return new GenericCommandResult<List<PlaceResponseDto>>(true, "Error", null, StatusCodes.Status400BadRequest);
        //}

    }
}
