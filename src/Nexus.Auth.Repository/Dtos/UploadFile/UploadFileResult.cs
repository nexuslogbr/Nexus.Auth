
using Nexus.Auth.Repository.Dtos.OrderService;

namespace Nexus.Auth.Repository.Dtos.UploadFile
{
    public class UploadFileResult
    {
        public OrderServiceDto OrderService { get; set; }
        public IList<UploadFileErrorDto> Errors { get; set; }
    }
}
