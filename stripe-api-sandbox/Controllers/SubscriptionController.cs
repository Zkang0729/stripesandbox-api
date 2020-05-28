using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Stripe;

namespace stripe_api_sandbox.Controllers
{
    [ApiController, Route("subscription"), Produces("application/json")]
    public class SubscriptionController : ControllerBase
    {
        private readonly SubscriptionService _subscriptionService;
        private readonly RequestOptions _requestOptions;

        public SubscriptionController(SubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
            _requestOptions = new RequestOptions
            {
                ApiKey = "sk_test_ThCObWFxHZdKzd7flb4xEm2200hvFDoAoy"
            };
        }

        [HttpGet]
        async public Task<StripeList<Subscription>> GetSubscriptionsAsync()
        {            
            return await _subscriptionService.ListAsync(null, _requestOptions);
        }

        [HttpGet("{id}")]
        async public Task<Subscription> GetSubscriptionAsync(string id)
        {
            return await _subscriptionService.GetAsync(id, null, _requestOptions);
        }

        [HttpPost]
        async public Task<Subscription> AddSubscriptionAsync([FromBody] SubscriptionCreateOptions subscriptionCreateOptions)
        {
            return await _subscriptionService.CreateAsync(subscriptionCreateOptions, _requestOptions);
        }

        [HttpPut("{id}")]
        async public Task<Subscription> UpdateSubscriptionAsync(string id, [FromBody] SubscriptionUpdateOptions subscriptionUpdateOptions)
        {
            return await _subscriptionService.UpdateAsync(id, subscriptionUpdateOptions, _requestOptions);
        }

        [HttpDelete("{id}/{invoiceNow}")]
        async public Task<Subscription> DeleteSubscriptionAsync(string id, bool invoiceNow)
        {
            var subscriptionCancelOptions = new SubscriptionCancelOptions
            {
                InvoiceNow = invoiceNow,
            };
            return await _subscriptionService.CancelAsync(id, subscriptionCancelOptions, _requestOptions);
        }
    }
}
