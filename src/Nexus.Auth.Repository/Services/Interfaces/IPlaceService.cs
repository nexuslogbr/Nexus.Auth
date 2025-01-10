using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Infra.Services.Interfaces;

namespace Nexus.Auth.Repository.Services.Interfaces
{
    public interface IPlaceService : IBaseDataService<Place>
    {
        Task<Place> GetByIdAsync(int id);
        Task<Place> GetByNameAsync(string name);
        Task<List<Place>> GetByIdsAsync(List<int> ids);
        Task<IList<Place>> GetByUserIdAsync(int userId);
    }
}
