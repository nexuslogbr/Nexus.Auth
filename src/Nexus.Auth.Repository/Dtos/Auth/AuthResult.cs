using Nexus.Auth.Repository.Dtos.Menu;
using Nexus.Auth.Repository.Dtos.Role;
using Nexus.Auth.Repository.Dtos.User;

namespace Nexus.Auth.Repository.Dtos.Auth
{
    public class AuthResult
    {
        public string? Token { get; set; }
        public UserResult? User { get; set; }
        public IList<RoleResult>? Roles { get; set; }
        public IList<MenuResult>? Menus { get; set; }
    }
}
