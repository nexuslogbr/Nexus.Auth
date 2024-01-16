
namespace Nexus.Auth.Repository.Dtos.Manufacturer
{
    public class ManufacturerResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ManufacturerWmiResponseDto> Wmis { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime RegisterDate { get; set; }

    }
}
