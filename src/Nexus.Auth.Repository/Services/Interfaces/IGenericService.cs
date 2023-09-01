using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Interfaces
{
    public interface IGenericService<T>
    {
        Task<GenericCommandResult> GetAll(PageParams pageParams, string path);
        Task<GenericCommandResult> GetById(GetById dto, string path);
        Task<GenericCommandResult> Delete(GetById obj, string path);
        Task<GenericCommandResult> GetByName(GetByName dto, string path);
        Task<GenericCommandResult> Post(T dto, string path);
        Task<GenericCommandResult> Put(T dto, string path);
    }
}
