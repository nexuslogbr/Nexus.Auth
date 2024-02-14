using Nexus.Auth.Repository.Dtos.Service;
using Nexus.Auth.Repository.Models;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Interfaces
{
    public interface IServiceService : IGenericService<ServiceDto, ServiceResponseDto>
    {
        Task<GenericCommandResult<List<ServiceResponseDto>>> GetByGamaType(GetByGamaTypeDto obj, string path);
    }
}
