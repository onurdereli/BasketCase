using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Basket.Models.Dtos;
using Basket.Models.Request;
using Basket.Services.Abstract;
using Shared.ControllerBase;

namespace Basket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : CustomBaseController
    {
        private readonly IBasketService _basketService;

        public BasketsController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetBasket(string userId)
        {
            return CreateActionResultInstance(await _basketService.GetBasketAysnc(userId));
        }

        [Route("[action]/{userId}", Name = "addbasketitem")]
        [HttpPost]
        public async Task<IActionResult> AddBasketItemAsync(string userId, BasketItemRequest basketItemRequest)
        {
            return CreateActionResultInstance(await _basketService.AddBasketItemAsync(userId, basketItemRequest));
        }

        [Route("{userId}/{productId}/{quantity}")]
        [HttpPut]
        public async Task<IActionResult> UpdateBasketItemAsync(string userId, string productId, int quantity)
        {
            return CreateActionResultInstance(await _basketService.UpdateBasketItemAsync(userId, productId, quantity));
        }

        [HttpDelete("{userId}/{productId}")]
        public async Task<IActionResult> RemoveBasketItemAsyc(string userId, string productId)
        {
            return CreateActionResultInstance(await _basketService.RemoveBasketItemAsync(userId, productId));
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteBasketAsync(string userId)
        {
            return CreateActionResultInstance(await _basketService.DeleteAysnc(userId));
        }

        [Route("[action]", Name = "checkout")]
        [HttpPost]
        public async Task<IActionResult> Checkout(BasketCheckoutDto basketCheckoutDto)
        {
            return CreateActionResultInstance(await _basketService.CheckoutBasket(basketCheckoutDto));
        }

        [Route("[action]/{userId}/{productId}/{price}", Name = "basketpricechangedtest")]
        [HttpGet]
        public async Task<IActionResult> BasketPriceChangedTest(string userId, string productId, decimal price)
        {
            return CreateActionResultInstance(await _basketService.BasketPriceChangedTest(userId, productId, price));
        }
    }
}
