using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.Place;

namespace Nexus.Auth.Repository.Dtos.User
{
    public class GetAllUserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IList<PlaceResult> Places { get; set; }
        public IList<GetById> Roles { get; set; }
        public DateTime ChangeDate { get; set; }
        public bool Blocked { get; set; }
    }
}
