using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stripe_api_sandbox.Controllers
{
    [ApiController, Route("subscriptionschedule"), Produces("application/json")]
    public class SubscriptionScheduleController
    {
        private readonly SubscriptionScheduleService _subscriptionScheduleService;
        private readonly RequestOptions _requestOptions;

        public SubscriptionScheduleController(SubscriptionScheduleService subscriptionScheduleService)
        {
            _subscriptionScheduleService = subscriptionScheduleService;
            _requestOptions = new RequestOptions
            {
                ApiKey = "sk_test_ThCObWFxHZdKzd7flb4xEm2200hvFDoAoy"
            };
        }

        [HttpGet("{id}")]
        async public Task<StripeList<SubscriptionSchedule>> GetSusbcriptionScheduleForACustomer(string id)
        {
            var subscriptionScheduleListOptions = new SubscriptionScheduleListOptions
            {
                Customer = id
            };
            return await _subscriptionScheduleService.ListAsync(subscriptionScheduleListOptions, _requestOptions);
        }

        [HttpPost]
        async public Task<string> AddSubscriptionScheduleAsync([FromBody] SubscriptionScheduleCreateOptions subscriptionScheduleCreateOptions = null)
        {
            subscriptionScheduleCreateOptions.StartDate = DateTime.UtcNow.AddYears(1);
            var res = await _subscriptionScheduleService.CreateAsync(subscriptionScheduleCreateOptions, _requestOptions);
            return res.Id;
        }


        [HttpDelete("{id}")]
        async public Task<string> CancelSubscriptionSchedule(string id)
        {
            var res = await _subscriptionScheduleService.CancelAsync(id, null, _requestOptions);
            return res.Id;
        }
    }
}
