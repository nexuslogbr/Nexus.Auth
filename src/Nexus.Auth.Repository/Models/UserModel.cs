using Nexus.Auth.Repository.Dtos.Generics;

namespace Nexus.Auth.Repository.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IList<GetById> Places { get; set; }
        public IList<GetById> Roles { get; set; }
        public DateTime ChangeDate { get; set; }
    }
}
