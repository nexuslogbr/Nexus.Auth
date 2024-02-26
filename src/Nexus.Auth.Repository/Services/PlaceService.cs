using Microsoft.Extensions.Configuration;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.Place;
using Nexus.Auth.Repository.Services.Interfaces;

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


        public async Task<PlaceResponseDto> GetById(int id)
        {
            return await _accessDataService.PostDataAsync<PlaceResponseDto>(_url, "Place/GetById", new GetById { Id = id });
        }

    }
}
