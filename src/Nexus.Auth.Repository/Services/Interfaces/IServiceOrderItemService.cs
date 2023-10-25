using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.ServiceOrderItem;
using Nexus.Auth.Repository.Params;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Services.Interfaces;

public interface IServiceOrderItemService
{
    Task<GenericCommandResult<PageList<ServiceOrderItemResponseDto>>> GetAll(ServiceOrderItemParams pageParams, string path);
    Task<GenericCommandResult<ServiceOrderItemResponseDto>> GetById(GetById dto, string path);
    Task<GenericCommandResult<ServiceOrderItemResponseDto>> UpdateChassisItems(ServiceOrderItemPutDto dto, string path);
}