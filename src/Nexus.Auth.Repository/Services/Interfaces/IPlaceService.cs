using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.Place;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Services.Interfaces
{
    public interface IPlaceService
    {
        Task<PlaceResponseDto?> GetById(int id);
    }
}
