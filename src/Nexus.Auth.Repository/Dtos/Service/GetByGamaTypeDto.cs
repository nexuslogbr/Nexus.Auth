using Nexus.Auth.Repository.Enums;
using System.ComponentModel.DataAnnotations;

namespace Nexus.Auth.Repository.Dtos.Service
{
    public class GetByGamaTypeDto
    {
        [Range(1, 2)]
        public GamaTypeEnum Type { get; set; }
    }
}
