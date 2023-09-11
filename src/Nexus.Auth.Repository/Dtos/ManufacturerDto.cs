
namespace Nexus.Auth.Repository.Dtos
{
    public  class ManufacturerDto
    {
        public required string Name { get; set; }
        public required List<WmiDto> WMIs { get; set; }
    }

    public class ManufacturerIdDto : ManufacturerDto
    {
        public required int Id { get; set; }
    }
}
