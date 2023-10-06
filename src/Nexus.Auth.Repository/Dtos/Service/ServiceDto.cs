using System.ComponentModel.DataAnnotations;

namespace Nexus.Auth.Repository.Dtos.Service
{
    public class ServiceDto
    {
        [Required]
        public string Name { get; set; }
        public string? Observation { get; set; }
        public IEnumerable<ServiceRelatedCustomerDto> ServiceRelatedCustomers { get; set; }
    }

    public class ServicePutDto : ServiceDto
    {
        [Required]
        public int Id { get; set; }
    }
}
