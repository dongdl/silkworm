using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using AutoMapper;
using DataModel;
using ATEC.Core.BusinessEntities;
using ATEC.Core.DataModel;
using ATEC.Core.DataModel.UnitOfWork;

namespace ATEC.Core.BusinessServices
{
    /// <summary>
    /// Offers services for customer specific CRUD operations
    /// </summary>
    public class CustomerServices:ICustomerServices
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public CustomerServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Fetches customer details by id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public CustomerEntity GetCustomerById(int customerId)
        {
            var customer = _unitOfWork.CustomerRepository.GetByID(customerId);
            if (customer != null)
            {
                Mapper.CreateMap<Customer, CustomerEntity>();
                var customerModel = Mapper.Map<Customer, CustomerEntity>(customer);
                return customerModel;
            }
            return null;
        }

        /// <summary>
        /// Fetches all the customers.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CustomerEntity> GetAllCustomers()
        {
            var customers = _unitOfWork.CustomerRepository.GetAll().ToList();
            if (customers.Any())
            {
                Mapper.CreateMap<Customer, CustomerEntity>();
                var customersModel = Mapper.Map<List<Customer>, List<CustomerEntity>>(customers);
                return customersModel;
            }
            return null;
        }

        /// <summary>
        /// Creates a customer
        /// </summary>
        /// <param name="customerEntity"></param>
        /// <returns></returns>
        public int CreateCustomer(CustomerEntity customerEntity)
        {
            using (var scope = new TransactionScope())
            {
                var customer = new Customer
                {
                    FullName = customerEntity.CustomerName
                };
                _unitOfWork.CustomerRepository.Insert(customer);
                _unitOfWork.Save();
                scope.Complete();
                return customer.Id;
            }
        }

        /// <summary>
        /// Updates a customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="customerEntity"></param>
        /// <returns></returns>
        public bool UpdateCustomer(int customerId, CustomerEntity customerEntity)
        {
            var success = false;
            if (customerEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var customer = _unitOfWork.CustomerRepository.GetByID(customerId);
                    if (customer != null)
                    {
                        customer.FullName = customerEntity.CustomerName;
                        _unitOfWork.CustomerRepository.Update(customer);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// Deletes a particular customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public bool DeleteCustomer(int customerId)
        {
            var success = false;
            if (customerId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var customer = _unitOfWork.CustomerRepository.GetByID(customerId);
                    if (customer != null)
                    {

                        _unitOfWork.CustomerRepository.Delete(customer);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }
    }
}
