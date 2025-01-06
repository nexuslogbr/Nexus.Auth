using Nexus.Auth.Domain.Enums;

namespace Nexus.Auth.Repository.Dtos.Menu
{
    public class MenuDto
    {
        public required string Name { get; set; }
        public string? Link { get; set; }
        public string? Icon { get; set; }
        public bool Mobile { get; set; }
        public MenuTypeEnum Type { get; set; }
        public int? MenuSectionId { get; set; }
    }

    public class MenuPutDto : MenuDto
    {
        public int Id { get; set; }
    }
}
