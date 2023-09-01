using Microsoft.AspNetCore.Http;
using Nexus.Auth.Repository.Dtos;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Interfaces;
using Nexus.Auth.Repository.Models;
using Nexus.Auth.Repository.Services.Interfaces;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Repository.Services
{
    public class CustomerService : ICustomerService<CustomerDto>
    {
        private readonly IAccessDataService _accessDataService;

        public CustomerService(IAccessDataService accessDataService)
        {
            _accessDataService = accessDataService ?? throw new ArgumentException(nameof(accessDataService));
        }

        public async Task<GenericCommandResult> GetAll(PageParams pageParams, string path)
        {
            var result = await _accessDataService.PostDataAsync<CustomerModel[]>(path, "/api/v1/Customer/GetAll", pageParams);
            if (result is not null)
                return new GenericCommandResult(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult(true, "Error", new object { }, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult> GetById(GetById obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<CustomerDto>(path, "/api/v1/Customer/GetById", obj);
            if (result is not null)
                return new GenericCommandResult(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult(true, "Error", new object { }, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult> GetByName(GetByName obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<TokenDto>(path, "/api/v1/Customer/GetByName", obj);
            if (result is not null)
                return new GenericCommandResult(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult(true, "Error", new object { }, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult> Post(CustomerDto obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<TokenDto>(path, "/api/v1/Customer/Post", obj);
            if (result is not null)
                return new GenericCommandResult(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult(true, "Error", new object { }, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult> Put(CustomerDto obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<TokenDto>(path, "/api/v1/Customer/Put", obj);
            if (result is not null)
                return new GenericCommandResult(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult(true, "Error", new object { }, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult> Delete(GetById obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<TokenDto>(path, "/api/v1/Customer/Delete", obj);
            if (result is not null)
                return new GenericCommandResult(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult(true, "Error", new object { }, StatusCodes.Status400BadRequest);
        }

    }
}
