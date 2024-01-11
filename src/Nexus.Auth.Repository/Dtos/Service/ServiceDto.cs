using Nexus.Auth.Repository.Dtos.Customer;
using Nexus.Auth.Repository.Enums;
using System.ComponentModel.DataAnnotations;

namespace Nexus.Auth.Repository.Dtos.Service
{
    public class ServiceDto
    {
        public required string Name { get; set; }
        public required decimal Value { get; set; }
        [Range(1, 2, ErrorMessage = "A gama deve ser 1 ou 2.")]
        public GamaTypeEnum GamaType { get; set; }

        public required int VpcItemId { get; set; }

        public required IList<CustomerVpcDto> ServiceCustomers { get; set; }
        public required IList<ServiceRequesterDto> ServiceRequesters { get; set; }
    }

    public class ServicePutDto : ServiceDto
    {
        [Required]
        public int Id { get; set; }
    }
}
