
namespace Nexus.Auth.Repository.Dtos.Place
{
    public class PlaceResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Acronym { get; set; }
        public string Country { get; set; }
        public string ChangeDate { get; set; }
        public bool Blocked { get; set; }
    }
}
