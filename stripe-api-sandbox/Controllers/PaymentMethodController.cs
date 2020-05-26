using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace stripe_api_sandbox.Controllers
{
    [ApiController, Route("paymentmethod"), Produces("application/json")]
    public class PaymentMethodController : ControllerBase
    {
        private readonly PaymentMethodService _paymentMethodService;
        private readonly RequestOptions _requestOptions;

        public PaymentMethodController(PaymentMethodService paymentMethodService)
        {
            _paymentMethodService = paymentMethodService;
            _requestOptions = new RequestOptions
            {
                ApiKey = "sk_test_ThCObWFxHZdKzd7flb4xEm2200hvFDoAoy"
            };
        }

        [HttpGet]
        async public Task<StripeList<PaymentMethod>> GetPaymentMethodsAsync([FromBody] PaymentMethodListOptions paymentMethodListOptions)
        {
            return await _paymentMethodService.ListAsync(paymentMethodListOptions, _requestOptions);
        }

        [HttpGet("{id}")]
        async public Task<PaymentMethod> GetPaymnetMethodAsync(string id)
        {
            return await _paymentMethodService.GetAsync(id, null, _requestOptions);
        }

        [HttpPost]
        async public Task<string> AddPaymentMethodAsync([FromBody] PaymentMethodCreateOptions paymentMethodCreateOptions)
        {
           /* var paymentMethodCreateOptions = new PaymentMethodCreateOptions
            {
                Type = "card",
                Card = new PaymentMethodCardCreateOptions { Token = "tok_1GiLeXGtzo9sPWie6SGoj08P" },
            };*/
            var res = await _paymentMethodService.CreateAsync(paymentMethodCreateOptions, _requestOptions);
            return res.Id;
        }

        [HttpPost("{id}")]
        async public Task<string> AttachPaymentMethodAsync(string id, [FromBody] PaymentMethodAttachOptions paymentMethodAttachOptions)
        {
            /*var paymentMethodAttachOptions = new PaymentMethodAttachOptions 
            { 
                Customer = "cus_HGEw5PjT0aCZmW"
            };*/
            var res = await _paymentMethodService.AttachAsync(id, paymentMethodAttachOptions, _requestOptions);
            return res.Id;
        }

        [HttpPut("{id}")]
        async public Task<PaymentMethod> UpdatePaymentMethodAsync(string id) // , [FromBody] PaymentMethodUpdateOptions paymentMethodUpdateOptions)
        {
            var paymentMethodUpdateOptions = new PaymentMethodUpdateOptions
            {
                BillingDetails = new BillingDetailsOptions { 
                    Email = "johntango@outlook.com"
                }
            };
            return await _paymentMethodService.UpdateAsync(id, paymentMethodUpdateOptions, _requestOptions);
        }
    }
}
