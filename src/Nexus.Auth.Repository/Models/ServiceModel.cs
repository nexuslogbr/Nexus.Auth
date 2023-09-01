
namespace Nexus.Auth.Repository.Models
{
    public class ServiceModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Observation { get; set; }
        public int GammaTypeId { get; set; }
        public string? Value { get; set; }
        public string? ChangeDate { get; set; }
        public bool Blocked { get; set; }
    }
}
