using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stripe_api_sandbox.Controllers
{
    [ApiController, Route("customer"), Produces("application/json")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;
        private readonly RequestOptions _requestOptions;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
            _requestOptions = new RequestOptions
            {
                ApiKey = "sk_test_ThCObWFxHZdKzd7flb4xEm2200hvFDoAoy"
            };
        }

        [HttpGet]
        async public Task<StripeList<Customer>> GetCustomersAsync()
        {
            return await _customerService.ListAsync(null, _requestOptions);
        }

        [HttpGet("{id}")]
        async public Task<string> GetCustomerAsync(string id)
        {
            var res = await _customerService.GetAsync(id, null, _requestOptions);
            return res.Name;
        }

        [HttpPost]
        async public Task<string> AddCustomerAsync([FromBody] CustomerCreateOptions customerCreateOptions)
        {
            var res = await _customerService.CreateAsync(customerCreateOptions, _requestOptions);
            return res.Id;
        }

        [HttpPut("{id}")]
        async public Task<Customer> UpdateCustomerAsync(string id, [FromBody] CustomerUpdateOptions customerUpdateOptions)
        {
            return await _customerService.UpdateAsync(id, customerUpdateOptions, _requestOptions);
        }

        [HttpDelete("{id}")]
        async public Task<Customer> DeleteCustomerAsync(string id)
        {
            return await _customerService.DeleteAsync(id, null, _requestOptions);
        }
    }
}
