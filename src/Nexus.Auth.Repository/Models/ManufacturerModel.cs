
namespace Nexus.Auth.Repository.Models
{
    public class ManufacturerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ManufacturerWMIModel> WMIs { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool Blocked { get; set; }
    }
}
