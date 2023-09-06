
namespace Nexus.Auth.Repository.Models
{
    public class ManufacturerModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required List<ManufacturerWMIModel> WMIs { get; set; }
        public required string ChangeDate { get; set; }
        public bool Blocked { get; set; }
    }
}
