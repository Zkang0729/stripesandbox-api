using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stripe_api_sandbox.Controllers
{
    [ApiController, Route("plan"), Produces("application/json")]
    public class PlanController : ControllerBase
    {
        private readonly PlanService _planService;
        private readonly RequestOptions _requestOptions;

        public PlanController(PlanService planService)
        {
            _planService = planService;
            _requestOptions = new RequestOptions
            {
                ApiKey = "sk_test_ThCObWFxHZdKzd7flb4xEm2200hvFDoAoy"
            };
        }

        [HttpGet]
        async public Task<StripeList<Plan>> GetPlansAsync()
        {
            return await _planService.ListAsync(null, _requestOptions);
        }

        [HttpGet("{id}")]
        async public Task<Plan> GetPlanAsync(string id)
        {
            return await _planService.GetAsync(id, null, _requestOptions);
        }
    }
}
