namespace Nexus.Auth.Repository.Dtos.Chassis;

public class ChassisResponseDto
{
    public int Id { get; set; }
    public string ChassisNumber { get; set; }
    public bool Blocked { get; set; }
    public DateTime RegisterDate { get; set; }
    public DateTime ChangeDate { get; set; }
}