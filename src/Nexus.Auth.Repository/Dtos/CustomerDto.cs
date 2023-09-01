using System.ComponentModel.DataAnnotations;

namespace Nexus.Auth.Repository.Dtos
{
    public class CustomerDto
    {
        [MaxLength(14)]
        [MinLength(14)]
        public required string TaxCode { get; set; }
        
        public required string Name { get; set; }
        public required AddressDto Address { get; set; }
        public required ContactDto[] Contacts { get; set; }
    }

    public class CustomerIdDto : CustomerDto
    {
        public required int Id { get; set; }
    }
}
