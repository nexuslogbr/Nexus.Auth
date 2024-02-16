using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.OrderService;
using Nexus.Auth.Repository.Interfaces;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Services.Interfaces
{
    public interface IOrderServiceService : IGenericService<OrderServiceToSaveDto, OrderServiceResponseDto>
    {
        Task<GenericCommandResult<OrderServiceResponseDto>> PostRange(List<OrderServiceToSaveDto> entities, string path);
        OrderServiceToSaveDto GetOrderData(OrderServiceDto data, int fileId);
        Task<GenericCommandResult<BooleanDto>> RemoveServicesRange(OrderServiceRemoveServicesDto obj, string path);
        Task<GenericCommandResult<OrderServiceServiceInfoDto>> GetServiceByIdAsync(GetById obj, string path);
        Task<GenericCommandResult<List<OrderServiceResponseDto>>> Filter(OrderServiceFilter obj, string path);
        Task<GenericCommandResult<BooleanDto>> CreateList(List<int> obj, string path);
        Task<GenericCommandResult<List<OrderServiceListFilterResponseDto>>> FilterLists(OrderServiceListFilter obj, string path);
        Task<GenericCommandResult<OrderServiceListResponseDto>> GetListById(GetById obj, string path);
        Task<GenericCommandResult<OrderServiceStreetsListDto>> GetUniqueStreetsAsync(GetByIdsDto obj, string path);
        Task<GenericCommandResult<List<OrderServiceResponseDto>>> GetOrdersbyStreetAsync(OrderServiceByStreetDto obj, string path);
        Task<GenericCommandResult<List<OrderServiceResponseDto>>> GetByChassi(GetByChassi obj, string path);
        Task<GenericCommandResult<OrderServiceResponseDto>> PutServicesOfOrder(OrderServiceUpdateDto obj, string path);
    }
}
