using Newtonsoft.Json;

namespace Nexus.Auth.Repository.Utils
{
    public class GenericCommandResult<T>
    {
        public GenericCommandResult() { }

        public GenericCommandResult(bool success, string message, T data, int statusCode)
        {
            Success = success;
            Message = message;
            Data = data;
            StatusCode = statusCode;
        }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }

        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }
        [JsonProperty("details")]
        public GenericCommandResultDetail? Detail { get; set; }
    }
}
