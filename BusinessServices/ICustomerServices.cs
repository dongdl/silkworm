using System.Collections.Generic;
using ATEC.Core.BusinessEntities;

namespace ATEC.Core.BusinessServices
{
    /// <summary>
    /// Customer Service Contract
    /// </summary>
    public interface ICustomerServices
    {
        CustomerEntity GetCustomerById(int customerId);
        IEnumerable<CustomerEntity> GetAllCustomers();
        int CreateCustomer(CustomerEntity customerEntity);
        bool UpdateCustomer(int customerId,CustomerEntity customerEntity);
        bool DeleteCustomer(int customerId);
    }
}
