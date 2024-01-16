using Nexus.Auth.Repository.Dtos.FileVpc;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.OrderService;
using Nexus.Auth.Repository.Interfaces;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Services.Interfaces
{
    public interface IFileVpcService : IGenericService<FileVpcDto, FileVpcResponseDto>
    {
        Task<GenericCommandResult<object>> PostRange(List<OrderServiceDto> entities, string path);
        Task<GenericCommandResult<FileVpcResponseDto>> GetByChassi(GetByChassi entity, string path);
        Task<GenericCommandResult<List<FileVpcResponseDto>>> GetAllByFileId(GetById dto, string path);
    }
}
