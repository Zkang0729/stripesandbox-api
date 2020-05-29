using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stripe_api_sandbox.Controllers
{
    [ApiController, Route("invoice"), Produces("application/json")]
    public class InvoiceController
    {
        private readonly InvoiceService _invoiceService;
        private readonly RequestOptions _requestOptions;

        public InvoiceController(InvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
            _requestOptions = new RequestOptions
            {
                ApiKey = "sk_test_ThCObWFxHZdKzd7flb4xEm2200hvFDoAoy"
            };
        }

        [HttpPost]
        async public Task<string> AddInvoiceAsync([FromBody] InvoiceCreateOptions invoiceCreateOptions = null)
        {
            var res = await _invoiceService.CreateAsync(invoiceCreateOptions, _requestOptions);
            return res.Id;
        }

        [HttpPost("{id}/pay")]
        async public Task<Invoice> ChargeInvoiceAsync(string id)
        {
            return await _invoiceService.PayAsync(id, null, _requestOptions);
        }
    }
}
