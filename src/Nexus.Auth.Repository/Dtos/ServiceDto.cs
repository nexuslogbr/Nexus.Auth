
namespace Nexus.Auth.Repository.Dtos
{
    public class ServiceDto
    {
        public required string Name { get; set; }
        public required double Value { get; set; }
        public required string Observation { get; set; }
        public required int GammaTypeId { get; set; }
    }

    public class ServiceIdDto : ServiceDto
    {
        public required int Id { get; set; }
    }
}
