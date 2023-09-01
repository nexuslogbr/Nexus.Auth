
namespace Nexus.Auth.Repository.Dtos
{
    public class AddressDto
    {
        public required string Street { get; set; }
        public required string Number { get; set; }
        public required string District { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required string ZipCode { get; set; }
    }
}
