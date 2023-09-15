using Nexus.Auth.Repository.Dtos;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Interfaces
{
    public interface IGenericService<TRequest, TResponse>
    {
        Task<GenericCommandResult<IEnumerable<TResponse>>> GetAll(PageParams pageParams, string path);
        Task<GenericCommandResult<TResponse>> GetById(GetById dto, string path);
        Task<GenericCommandResult<TokenDto>> Delete(GetById obj, string path);
        Task<GenericCommandResult<IEnumerable<TResponse>>> GetByName(GetByName dto, string path);
        Task<GenericCommandResult<TResponse>> Post(TRequest dto, string path);
        Task<GenericCommandResult<TResponse>> Put(TRequest dto, string path);
    }
}
