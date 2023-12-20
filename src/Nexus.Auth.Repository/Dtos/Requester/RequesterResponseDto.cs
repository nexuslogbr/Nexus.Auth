namespace Nexus.Auth.Repository.Dtos.Requester;

public class RequesterResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public bool Blocked { get; set; }
    public DateTime RegisterDate { get; set; }
    public DateTime ChangeDate { get; set; }
}