using System.ComponentModel;

namespace Nexus.Auth.Repository.Dtos.Menu
{
    public class MenuDto
    {
        public string Name { get; set; }

        [DefaultValue(false)]
        public bool Mobile { get; set; }
    }

    public class MenuIdDto : MenuDto
    {
        public int Id { get; set; }
    }
}

