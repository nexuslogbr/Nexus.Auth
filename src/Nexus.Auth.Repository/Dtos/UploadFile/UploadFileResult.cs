
namespace Nexus.Auth.Repository.Dtos.UploadFile
{
    public class UploadFileResult
    {
        public string Data { get; set; }
        public IList<UploadFileErrorDto> Errors { get; set; }
    }
}
