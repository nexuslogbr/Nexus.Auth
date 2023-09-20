namespace Nexus.Auth.Repository.Dtos;

public class CategoryResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Blocked { get; set; }
    public DateTime RegisterDate { get; set; }
    public DateTime ChangeDate { get; set; }
}