using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Nexus.Auth.Repository.Exceptions;
using Nexus.Auth.Repository.Services.Interfaces;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Nexus.Auth.Repository.Services
{
    public class AccessDataService : IAccessDataService
    {
        private readonly ILogger<AccessDataService> _logger;
        private readonly HttpClient _httpClient;
        private HttpClientHandler handler = new HttpClientHandler();

        public AccessDataService(ILogger<AccessDataService> logger, HttpClient httpClient)
        {
            handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<T> PostFormDataAsync<T>(string path, string url, object data) where T : class
        {
            var responseString = string.Empty;

            try
            {
                if (!string.IsNullOrEmpty(url))
                {
                    _httpClient.BaseAddress = new Uri(path);
                    using var formContent = new MultipartFormDataContent();
                    foreach (var prop in data.GetType().GetProperties())
                    {
                        if (prop.GetType().Equals(typeof(IFormFile)))
                        {
                            var fileName = (prop.GetValue(prop) as IFormFile).Name;
                            formContent.Add(new ByteArrayContent(File.ReadAllBytes(prop.GetValue(prop).ToString())),
                                prop.Name,
                                fileName);
                        }
                        else
                        {
                            formContent.Add(new MultipartFormDataContent(prop.GetValue(prop).ToString()),
                                String.Format("\"{0}\"", prop.Name));
                        }
                    }
                    var httpResponse = await _httpClient.PostAsync(url, formContent);
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

        public async Task<T> PostDataAsync<T>(string path, string url, object data) where T : class
        {
            var responseString = string.Empty;

            try
            {
                if (!string.IsNullOrEmpty(url))
                {
                    _httpClient.BaseAddress = new Uri(path);
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var httpResponse = await _httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
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
