using Nexus.Auth.Domain.Enums;

namespace Nexus.Auth.Repository.Dtos.Menu
{
    public class MenuResult
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public bool Mobile { get; set; }
        public string? Link { get; set; }
        public string? Icon { get; set; }
        public int? MenuSectionId { get; set; }
        public MenuTypeEnum Type { get; set; }
    }
}
