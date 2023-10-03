using Nexus.Auth.Repository.Dtos.Generics;

namespace Nexus.Auth.Repository.Handlers.Interfaces
{
    public interface IBaseHandler<T>
    {
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<bool> Delete(int id);
        Task<T> DeleteRange(T[] entity);
        Task<T> SaveChangesAsync();
    }
}
