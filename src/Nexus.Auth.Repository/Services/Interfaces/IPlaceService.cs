using Nexus.Auth.Repository.Dtos.Place;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Services.Interfaces
{
    public interface IPlaceService
    {
        Task<GenericCommandResult<PlaceResponseDto?>> GetById(int id);
        Task<GenericCommandResult<List<PlaceResponseDto>>> GetByIds(List<int> dto);
    }
}
