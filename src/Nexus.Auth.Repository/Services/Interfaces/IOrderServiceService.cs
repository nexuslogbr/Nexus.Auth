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
    }
}
