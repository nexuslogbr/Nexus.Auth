using Nexus.Auth.Repository.Dtos.Generics;

namespace Nexus.Auth.Repository.Services.Interfaces
{
    public interface IBaseService<T>
    {
        Task<PageList<T>> GetAllAsync(PageParams pageParams);
        Task<bool> Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(int id);
        Task<bool> DeleteRange(int id);
        Task<bool> SaveChangesAsync();
    }
}
