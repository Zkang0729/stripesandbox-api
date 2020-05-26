using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stripe_api_sandbox.Controllers
{
    [ApiController, Route("card"), Produces("application/json")]
    public class CardController
    {
        private readonly CardService _cardService;
        private readonly RequestOptions _requestOptions;

        public CardController(CardService cardService)
        {
            _cardService = cardService;
            _requestOptions = new RequestOptions
            {
                ApiKey = "sk_test_ThCObWFxHZdKzd7flb4xEm2200hvFDoAoy"
            };
        }

        [HttpGet("{customerId}")]
        async public Task<StripeList<Card>> GetCardsAsync(string customerId)
        {
            return await _cardService.ListAsync(customerId, null, _requestOptions);
        }

        [HttpGet("{customerId}/{cardId}")]
        async public Task<Card> GetCardAsync(string customerId, string cardId)
        {
            return await _cardService.GetAsync(customerId, cardId,  null, _requestOptions);
        }

        [HttpPost("{customerId}")]
        async public Task<Card> AddCardAsync(string customerId, string sourceToken, CardCreateNestedOptions cardCreateNestedOptions = null)
        {
            var cardCreateOptions = new CardCreateOptions
            {
                // Source = sourceToken,
                Source = "tok_1Gi5ZrGtzo9sPWiehTh9iGzT",
            };
            return await _cardService.CreateAsync(customerId, cardCreateOptions, _requestOptions);
        }

        [HttpPut("{customerId}/{cardId}")]
        async public Task<Card> UpdateCardAsync(string customerId, string cardId, [FromBody] CardUpdateOptions cardUpdateOptions)
        {
            return await _cardService.UpdateAsync(customerId, cardId, cardUpdateOptions, _requestOptions);
        }

        [HttpDelete("{customerId}/{cardId}")]
        async public Task<Card> DeleteCardAsync(string cutomerId, string cardId)
        {
            return await _cardService.DeleteAsync(cutomerId, cardId, null, _requestOptions);
        }
    }
}
