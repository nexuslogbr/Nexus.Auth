using Nexus.Auth.Repository.Dtos.Customer;
using Nexus.Auth.Repository.Dtos.Requester;
using Nexus.Auth.Repository.Dtos.VpcItem;
using Nexus.Auth.Repository.Enums;

namespace Nexus.Auth.Repository.Dtos.Service;

public class ServiceResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Value { get; set; }
    public List<CustomerVpcDto> Customers { get; set; }
    public List<RequesterResponseDto> Requesters { get; set; }
    public VpcItemModelResponseDto VpcItem { get; set; }
    public GamaTypeEnum GamaType { get; set; }
    public bool Blocked { get; set; }
    public DateTime RegisterDate { get; set; }
    public DateTime ChangeDate { get; set; }
}