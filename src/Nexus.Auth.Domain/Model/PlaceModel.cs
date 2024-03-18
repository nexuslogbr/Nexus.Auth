
namespace Nexus.Auth.Domain.Model
{
    public class PlaceModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Acronym { get; set; }
        public string Country { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime ChangeDate { get; set; }
        public bool Blocked { get; set; }
    }
}
