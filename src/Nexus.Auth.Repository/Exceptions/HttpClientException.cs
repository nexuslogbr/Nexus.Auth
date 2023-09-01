
namespace Nexus.Auth.Repository.Exceptions
{
    public class HttpClientException : Exception
    {
        public new string Message { get; private set; }
        public new string ResponseString { get; private set; }

        public HttpClientException(string message = "", string responseString = "") : base(message)
        {
            Message = message;
            ResponseString = responseString;
        }
    }
}
