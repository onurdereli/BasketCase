using System.Threading.Tasks;
using Basket.Models.Dtos;
using Basket.Models.Models;
using Basket.Models.Request;
using Shared.Dtos;

namespace Basket.Services.Abstract
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> GetBasketAysnc(string userId);

        Task<Response<bool>> AddBasketItemAsync(string userId, BasketItemRequest basketItemRequest);

        Task<Response<bool>> UpdateBasketItemAsync(string userId, string productId, int quantity);

        Task<Response<bool>> RemoveBasketItemAsync(string userId, string productId);

        Task<Response<bool>> DeleteAysnc(string userId);

        Task<Response<bool>> SaveOrUpdateAsync(BasketDto basketDto);

        Task<Response<bool>> CheckoutBasket(BasketCheckoutDto basketCheckoutDto);

        Task<Response<bool>> BasketPriceChangedTest(string userId, string productId, decimal price);
    }
}
