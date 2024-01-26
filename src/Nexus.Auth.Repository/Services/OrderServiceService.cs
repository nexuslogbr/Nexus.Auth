using Microsoft.AspNetCore.Http;
using Nexus.Auth.Domain.Enums;
using Nexus.Auth.Repository.Dtos.Auth;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.OrderService;
using Nexus.Auth.Repository.Enums;
using Nexus.Auth.Repository.Services.Interfaces;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Services
{
    public class OrderServiceService : IOrderServiceService
    {
        private readonly IAccessDataService _accessDataService;

        public OrderServiceService(IAccessDataService accessDataService)
        {
            _accessDataService = accessDataService;
        }

        public async Task<GenericCommandResult<PageList<OrderServiceResponseDto>>> GetAll(PageParams pageParams, string path)
        {
            var result = await _accessDataService.PostDataAsync<PageList<OrderServiceResponseDto>>(path, "api/v1/OrderService/GetAll", pageParams);
            if (result is not null)
                return new GenericCommandResult<PageList<OrderServiceResponseDto>>(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult<PageList<OrderServiceResponseDto>>(true, "Error", default, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult<OrderServiceResponseDto>> GetById(GetById obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<OrderServiceResponseDto>(path, "api/v1/OrderService/GetById", obj);
            if (result is not null)
                return new GenericCommandResult<OrderServiceResponseDto>(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult<OrderServiceResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult<OrderServiceResponseDto>> GetByName(GetByName obj, string path)
        {
            throw new NotImplementedException();
        }

        public async Task<GenericCommandResult<OrderServiceResponseDto>> Post(OrderServiceToSaveDto obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<OrderServiceResponseDto>(path, "api/v1/OrderService/Post", obj);
            if (result is not null)
                return new GenericCommandResult<OrderServiceResponseDto>(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult<OrderServiceResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult<OrderServiceResponseDto>> PostRange(List<OrderServiceToSaveDto> entities, string path)
        {
            var result = await _accessDataService.PostDataAsync<OrderServiceResponseDto>(path, "api/v1/OrderService/PostRange", entities);
            if (result is not null)
                return new GenericCommandResult<OrderServiceResponseDto>(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult<OrderServiceResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult<OrderServiceResponseDto>> Put(OrderServiceToSaveDto obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<OrderServiceResponseDto>(path, "api/v1/OrderService/Put", obj);
            if (result is not null)
                return new GenericCommandResult<OrderServiceResponseDto>(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult<OrderServiceResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult<TokenDto>> Delete(GetById obj, string path)
        {
            var result = await _accessDataService.PostDataAsync<TokenDto>(path, "api/v1/OrderService/Delete", obj);
            if (result is not null)
                return new GenericCommandResult<TokenDto>(true, "Success", result, StatusCodes.Status200OK);

            return new GenericCommandResult<TokenDto>(true, "Error", default, StatusCodes.Status400BadRequest);
        }

        public async Task<GenericCommandResult<ChangeStatusDto>> ChangeStatus(ChangeStatusDto obj, string path)
        {
            throw new NotImplementedException();
        }

        #region

        public OrderServiceToSaveDto GetOrderData(OrderServiceDto data, int fileId)
        {
            return new OrderServiceToSaveDto 
            {
                Chassi = data.Chassis,
                CustomerId = data.CustomerId,
                CustomerName = data.Customer,
                RequesterId = data.RequesterId,
                RequesterName = data.Requester,
                RequesterCode = data.RequesterCode,
                Services = data.Services.Select(x => new OrderServicesDto { ServiceId = x.ServiceId}).ToList(),
                OrderStatus = OrderServiceStatusEnum.Created,
                ManufacturerId = data.ManufacturerId,
                ManufacturerName = data.ManufacturerName,
                ModelId = data.ModelId,
                ModelName = data.ModelName,
                Invoicing = data.Invoicing ?? new DateTime(),
                Street = data.Street,
                Parking = data.Parking ?? 0,
                Plate = data.Plate,
                Place = data.Place,
                FileId = fileId
            };
        }

        #endregion
    }
}
