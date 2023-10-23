using Nexus.Auth.Repository.Dtos.Customer;
using Nexus.Auth.Repository.Models;

namespace Nexus.Auth.Repository.Interfaces
{
    public interface ICustomerService : IGenericService<CustomerDto, CustomerModel> 
    {
    }
}
