using Microsoft.AspNetCore.Http;
using Nexus.Auth.Repository.Dtos.Auth;
using Nexus.Auth.Repository.Dtos.Customer;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Interfaces;
using Nexus.Auth.Repository.Models;
using Nexus.Auth.Repository.Services.Interfaces;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Repository.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IAccessDataService _accessDataService;

        public CustomerService(IAccessDataService accessDataService)
        {
            _accessDataService = accessDataService ?? throw new ArgumentException(nameof(accessDataService));
        }

        public async Task<GenericCommandResult<PageList<CustomerModel>>> GetAll(PageParams pageParams, string path)
        {
            var result = await _accessDataService.PostDataAsync<PageList<CustomerModel>>(path, "api/v1/Customer/GetAll", pageParams);
            if (result is not null)
                return new GenericCommandResult<PageList<CustomerModel>>(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult<PageList<CustomerModel>>(true, "Error", default, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult<CustomerModel>> GetById(GetById obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<CustomerModel>(path, "api/v1/Customer/GetById", obj);
            if (result is not null)
                return new GenericCommandResult<CustomerModel>(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult<CustomerModel>(true, "Error", default, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult<CustomerModel>> GetByName(GetByName obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<CustomerModel>(path, "api/v1/Customer/GetByName", obj);
            if (result is not null)
                return new GenericCommandResult<CustomerModel>(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult<CustomerModel>(true, "Error", default, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult<ChangeStatusDto>> ChangeStatus(ChangeStatusDto obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<ChangeStatusDto>(path, "api/v1/Customer/ChangeStatus", obj);
            if (result is not null)
                return new GenericCommandResult<ChangeStatusDto>(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult<ChangeStatusDto>(true, "Error", default, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult<CustomerModel>> Post(CustomerDto obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<CustomerModel>(path, "api/v1/Customer/Post", obj);
            if (result is not null)
                return new GenericCommandResult<CustomerModel>(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult<CustomerModel>(true, "Error", default, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult<CustomerModel>> Put(CustomerDto obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<CustomerModel>(path, "api/v1/Customer/Put", obj);
            if (result is not null)
                return new GenericCommandResult<CustomerModel>(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult<CustomerModel>(true, "Error", default, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult<TokenDto>> Delete(GetById obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<TokenDto>(path, "api/v1/Customer/Delete", obj);
            if (result is not null)
                return new GenericCommandResult<TokenDto>(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult<TokenDto>(true, "Error", default, StatusCodes.Status400BadRequest);
        }

    }
}
