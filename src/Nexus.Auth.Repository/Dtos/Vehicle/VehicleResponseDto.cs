namespace Nexus.Auth.Repository.Dtos.VehicleInfo;

public class VehicleResponseDto
{
    public int Id { get; set; }
    public string RegisterDate { get; set; }
    public string ChangeDate { get; set; }

    public string Chassis { get; set; }
    public int FileId { get; set; }
    public int PlaceId { get; set; }
    public bool Blocked { get; set; }
    public int ModelId { get; set; }
}