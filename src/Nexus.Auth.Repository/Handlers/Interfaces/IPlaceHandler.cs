using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.Place;
using Nexus.Auth.Repository.Models;
using Nexus.Auth.Repository.Params;

namespace Nexus.Auth.Repository.Handlers.Interfaces
{
    public interface IPlaceHandler
    {
        Task<PlaceModel> Add(PlaceDto entity);
        Task<PlaceModel> Update(PlacePutDto entity);
        Task<bool> Delete(int id);
        Task<PageList<PlaceModel>> GetAll(PlaceParams pageParams, int placeId);
        Task<PlaceModel> GetById(int id);
        Task<PlaceModel> GetByName(string name);
        Task<ChangeStatusDto> ChangeStatus(ChangeStatusDto dto);
        Task<List<PlaceModel>> GetByIds(List<int> ids);
    }
}
