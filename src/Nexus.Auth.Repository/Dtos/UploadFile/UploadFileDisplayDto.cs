namespace Nexus.Auth.Repository.Dtos.UploadFile
{
    public class UploadFileDisplayDto
    {
        public string FileName { get; set; }
        public string Data { get; set; }
        public int ConcludedRegisters { get; set; }
        public int FailedRegisters { get; set; }
    }
}
