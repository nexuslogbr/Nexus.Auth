
namespace Nexus.Auth.Domain.Entities
{
    public class UserPlace : EntityBase
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int PlaceId { get; set; }
    }
}
