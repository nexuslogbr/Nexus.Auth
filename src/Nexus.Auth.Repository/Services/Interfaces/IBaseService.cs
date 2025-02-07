using Nexus.Auth.Repository.Dtos.Generics;

namespace Nexus.Auth.Repository.Services.Interfaces
{
    public interface IBaseService<T>
    {
        Task<List<T>> GetAllAsync(PageParams pageParams, int placeId);
        Task<bool> Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(int id);
        Task<bool> DeleteRange(int id);
        Task<bool> SaveChangesAsync();
    }
}
