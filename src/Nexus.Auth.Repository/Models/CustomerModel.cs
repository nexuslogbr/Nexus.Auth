
using Nexus.Auth.Repository.Dtos.Customer;

namespace Nexus.Auth.Repository.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? TaxCode { get; set; }
        public string? ChangeDate { get; set; }
        public bool Blocked { get; set; }
        public AddressDto? Address { get; set; }
        public ContactDto[]? Contacts { get; set; }
    }
}
