using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Nexus.Auth.Repository.Exceptions;
using Nexus.Auth.Repository.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text;

namespace Nexus.Auth.Repository.Services
{
    public class AccessDataService : IAccessDataService
    {
        private readonly ILogger<AccessDataService> _logger;

        public AccessDataService() { }
        public AccessDataService(ILogger<AccessDataService> logger)
        {
            _logger = logger;
        }

        public async Task<T> PostDataAsync<T>(string path, string url, object data) where T : class
        {
            var responseString = string.Empty;

            try
            {
                if (!string.IsNullOrEmpty(url))
                {
                    var httpClient = new HttpClient { BaseAddress = new Uri(path) };

                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var httpResponse = await httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
                    responseString = await httpResponse.Content.ReadAsStringAsync();

                    var content = !string.IsNullOrEmpty(responseString) ? JsonConvert.DeserializeObject<T>(responseString) : Activator.CreateInstance<T>();

                    return content;
                }
                else
                    throw new HttpClientException("Url cannot is null", responseString);

            }
            catch (JsonSerializationException e)
            {
                throw new HttpClientException(e.Message, responseString);
            }
            catch (HttpRequestException e)
            {
                throw new HttpClientException(e.Message, responseString);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error sending POST request to service at {url}: {e.Message}");
                _logger.LogError($"Error sending POST request to service at Content: {responseString}");
                throw;
            }
        }

        public async Task<T> GetDataAsync<T>(string path, string url) where T : class
        {
            var responseString = string.Empty;

            try
            {
                if (!string.IsNullOrEmpty(url))
                {
                    var httpClient = new HttpClient { BaseAddress = new Uri(path), };

                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var httpResponse = await httpClient.GetAsync(url);
                    responseString = await httpResponse.Content.ReadAsStringAsync();

                    var content = !string.IsNullOrEmpty(responseString) ? JsonConvert.DeserializeObject<T>(responseString) : Activator.CreateInstance<T>();

                    return content;
                }
                else
                    throw new HttpClientException("Error", responseString);
            }
            catch (JsonSerializationException e)
            {
                throw new HttpClientException(e.Message, responseString);
            }
            catch (HttpRequestException e)
            {
                throw new HttpClientException(e.Message, responseString);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error sending GET request to service at {url}: {e.Message}");
                _logger.LogError($"Error sending GET request to service at Content: {responseString}");
                throw;
            }
        }

    }
}
