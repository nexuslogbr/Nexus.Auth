
namespace Nexus.Auth.Repository.Services.Interfaces
{
    public interface IAccessDataService
    {
        Task<T> PostDataAsync<T>(string path, string url, object data) where T : class;
        Task<T> GetDataAsync<T>(string path, string url) where T : class;
    }
}
