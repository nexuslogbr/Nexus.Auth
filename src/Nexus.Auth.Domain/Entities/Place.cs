
namespace Nexus.Auth.Domain.Entities
{
    public class Place : EntityBase
    {
        public required string Name { get; set; }
        public required string Acronym { get; set; }
        public required string Country { get; set; }
    }
}
