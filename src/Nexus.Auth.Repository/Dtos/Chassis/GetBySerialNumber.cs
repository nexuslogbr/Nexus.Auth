using System.ComponentModel.DataAnnotations;

namespace Nexus.Auth.Repository.Dtos.Chassis
{
    public class GetBySerialNumber
    {
        [MaxLength(6)]
        [MinLength(6)]
        public required string SerialNumber { get; set; }
    }
}
