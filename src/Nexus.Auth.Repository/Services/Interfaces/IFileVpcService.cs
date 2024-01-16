using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.OrderService;
using Nexus.Auth.Repository.Dtos.UploadFile;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Services.Interfaces
{
    public interface IFileVpcService
    {
        Task<GenericCommandResult<object>> PostRange(List<OrderServiceDto> entities, string path);
        Task<GenericCommandResult<FileVpcResponseDto>> GetByChassi(GetByChassi entity, string path);
    }
}
