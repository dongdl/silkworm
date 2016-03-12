using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AttributeRouting;
using AttributeRouting.Web.Http;
using ATEC.Core.BusinessEntities;
using ATEC.Core.BusinessServices;
using ATEC.Silkworm.ActionFilters;
using ATEC.Silkworm.ErrorHelper;

namespace ATEC.Silkworm.Controllers
{
    [AuthorizationRequired]
    [RoutePrefix("v1/Customers/Customer")]
    public class CustomerController : ApiController
    {
        #region Private variable.

        private readonly ICustomerServices _customerServices;

        #endregion

        #region Public Constructor

        /// <summary>
        /// Public constructor to initialize customer service instance
        /// </summary>
        public CustomerController(ICustomerServices customerServices)
        {
            _customerServices = customerServices;
        }

        #endregion

        // GET api/customer
        [GET("allcustomers")]
        [GET("all")]
        public HttpResponseMessage Get()
        {
            var customers = _customerServices.GetAllCustomers();
            var customerEntities = customers as List<CustomerEntity> ?? customers.ToList();
            if (customerEntities.Any())
                return Request.CreateResponse(HttpStatusCode.OK, customerEntities);
            throw new ApiDataException(1000, "Customers not found", HttpStatusCode.NotFound);
        }

        // GET api/customer/5
        [GET("customerid/{id?}")]
        [GET("particularcustomer/{id?}")]
        [GET("mycustomer/{id:range(1, 3)}")]
        public HttpResponseMessage Get(int id)
        {
            if (id > 0)
            {
                var customer = _customerServices.GetCustomerById(id);
                if (customer != null)
                    return Request.CreateResponse(HttpStatusCode.OK, customer);

                throw new ApiDataException(1001, "No customer found for this id.", HttpStatusCode.NotFound);
            }
            throw new ApiException() { ErrorCode = (int)HttpStatusCode.BadRequest, ErrorDescription = "Bad Request..." };
        }

        // POST api/customer
        [POST("Create")]
        [POST("Register")]
        public int Post([FromBody] CustomerEntity customerEntity)
        {
            return _customerServices.CreateCustomer(customerEntity);
        }

        // PUT api/customer/5
        [PUT("Update/customerid/{id}")]
        [PUT("Modify/customerid/{id}")]
        public bool Put(int id, [FromBody] CustomerEntity customerEntity)
        {
            return id > 0 && _customerServices.UpdateCustomer(id, customerEntity);
        }

        // DELETE api/customer/5
        [DELETE("remove/customerid/{id}")]
        [DELETE("clear/customerid/{id}")]
        [PUT("delete/customerid/{id}")]
        public bool Delete(int id)
        {
            if (id > 0)
            {
                var isSuccess = _customerServices.DeleteCustomer(id);
                if (isSuccess)
                {
                    return true;
                }
                throw new ApiDataException(1002, "Customer is already deleted or not exist in system.", HttpStatusCode.NoContent );
            }
            throw new ApiException() {ErrorCode = (int) HttpStatusCode.BadRequest, ErrorDescription = "Bad Request..."};
        }
    }
}
