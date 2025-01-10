using Nexus.Auth.Repository.Dtos.Generics;

namespace Nexus.Auth.Repository.Dtos.User
{
    public class UserDto
    {
        public required string Name { get; set; }
        public required string UserName { get; set; }
        public required IList<GetById> Roles { get; set; }
        public required IList<GetById> Places { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class UserPutDto : UserDto
    {
        public int Id { get; set; }
    }
}
