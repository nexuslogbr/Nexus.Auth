
namespace Nexus.Auth.Repository.Dtos.Place
{
    public class PlaceDto
    {
        public required string Name { get; set; }
        public required string Acronym { get; set; }
        public required string Country { get; set; }
    }

    public class PlacePutDto : PlaceDto
    {
        public required int Id { get; set; }
    }
}
