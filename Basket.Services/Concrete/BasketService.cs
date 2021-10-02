using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Basket.Core.Constants;
using Basket.Core.Utilities.Business;
using Basket.Data.Repository.Abstract;
using Basket.Models.Dtos;
using Basket.Models.Models;
using Basket.Models.Request;
using Basket.Services.Abstract;
using MassTransit;
using Shared.Messages.Events.Abstract;
using Shared.Messages.Events.Concrete;

namespace Basket.Services.Concrete
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;


        public BasketService(IBasketRepository basketRepository, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Shared.Dtos.Response<BasketDto>> GetBasketAysnc(string userId)
        {
            var existBasket = await _basketRepository.GetAysnc(userId);
            
            return existBasket == null ? Shared.Dtos.Response<BasketDto>.Fail(BasketMessages.BasketNotFoundMessage, HttpStatusCode.NotFound) : Shared.Dtos.Response<BasketDto>.Success(existBasket, HttpStatusCode.OK);
        }

        public async Task<Shared.Dtos.Response<bool>> AddBasketItemAsync(string userId, BasketItemRequest basketItemRequest)
        {
            var addBasket = _mapper.Map<BasketItem>(basketItemRequest);

            var rulesResponse = BusinessRules.Run(IsEnoughStockAvailable(addBasket.ProductId, addBasket.Quantity));

            if (!rulesResponse.IsSuccessfull)
            {
                return Shared.Dtos.Response<bool>.Fail(rulesResponse.Errors, (HttpStatusCode)rulesResponse.StatusCode);
            }

            var basketRespose = await _basketRepository.GetAysnc(userId);

            var basketData = _mapper.Map<Models.Models.Basket>(basketRespose);

            if (basketData != null)
            {
                if (basketData.BasketItems.Any(item => item.ProductId == basketItemRequest.ProductId))
                {
                    return Shared.Dtos.Response<bool>.Success(HttpStatusCode.NoContent);
                }

                basketData.BasketItems.Add(addBasket);
            }
            else
            {
                basketData = new Models.Models.Basket()
                {
                    UserId = userId
                };
                basketData.BasketItems.Add(addBasket);
            }

            var saveOrUpdate = await SaveOrUpdate(basketData);

            return saveOrUpdate.IsSuccessfull ? Shared.Dtos.Response<bool>.Success(HttpStatusCode.Created) : Shared.Dtos.Response<bool>.Fail(BasketMessages.NotAddBasketMessage, (HttpStatusCode)saveOrUpdate.StatusCode);
        }

        public async Task<Shared.Dtos.Response<bool>> UpdateBasketItemAsync(string userId, string productId, int quantity)
        {
            var basketRespose = await _basketRepository.GetAysnc(userId);

            if (basketRespose == null)
            {
                return Shared.Dtos.Response<bool>.Fail(BasketMessages.NotUpdateBasketMessage, HttpStatusCode.NotFound);
            }

            var basketData = _mapper.Map<Models.Models.Basket>(basketRespose);

            var basketItem = basketData.BasketItems.Find(item => item.ProductId == productId);

            if (basketItem == null)
            {
                return Shared.Dtos.Response<bool>.Fail(BasketMessages.NotFoundBasketForUpdateMessage, HttpStatusCode.NotFound);
            }

            var rulesResponse = BusinessRules.Run(IsEnoughStockAvailable(basketItem.ProductId, quantity));

            if (!rulesResponse.IsSuccessfull)
            {
                return Shared.Dtos.Response<bool>.Fail(rulesResponse.Errors, (HttpStatusCode)rulesResponse.StatusCode);
            }

            basketItem.Quantity = quantity;

            return await SaveOrUpdate(basketData);
        }

        public async Task<Shared.Dtos.Response<bool>> RemoveBasketItemAsync(string userId, string productId)
        {
            var basketRespose = await _basketRepository.GetAysnc(userId);

            var basketData = _mapper.Map<Models.Models.Basket>(basketRespose);

            var deleteBasketItem = basketData?.BasketItems.FirstOrDefault(item => item.ProductId == productId);

            if (deleteBasketItem == null)
            {
                return Shared.Dtos.Response<bool>.Fail(BasketMessages.NotFoundProductByProductCodeMessage, HttpStatusCode.NotFound);
            }

            var isDeleteBasket = basketData.BasketItems.Remove(deleteBasketItem);

            if (!isDeleteBasket)
            {
                return Shared.Dtos.Response<bool>.Fail(BasketMessages.ProductCouldNotRemoveMessage, HttpStatusCode.BadRequest);
            }

            if(basketData.BasketItems.Count == 0)
            {
                return await DeleteAysnc(userId);
            }

            return await SaveOrUpdate(basketData);
        }

        public async Task<Shared.Dtos.Response<bool>> SaveOrUpdateAsync(BasketDto basketDto)
        {
            var status = await SaveOrUpdate(_mapper.Map<Models.Models.Basket>(basketDto));

            return status.IsSuccessfull ? Shared.Dtos.Response<bool>.Success((HttpStatusCode)status.StatusCode) : Shared.Dtos.Response<bool>.Fail(BasketMessages.BasketCouldNotUpdateOrSaveMessage, (HttpStatusCode)status.StatusCode);
        }

        private async Task<Shared.Dtos.Response<bool>> SaveOrUpdate(Models.Models.Basket basket)
        {
            var status = await _basketRepository.SaveOrUpdateAsyc(basket);

            return status ? Shared.Dtos.Response<bool>.Success(HttpStatusCode.NoContent) : Shared.Dtos.Response<bool>.Fail(BasketMessages.BasketCouldNotUpdateOrSaveMessage, HttpStatusCode.BadRequest);
        }

        public async Task<Shared.Dtos.Response<bool>> DeleteAysnc(string userId)
        {
            var deleteStatus = await _basketRepository.DeleteAysnc(userId);

            return deleteStatus ? Shared.Dtos.Response<bool>.Success(HttpStatusCode.NoContent) : Shared.Dtos.Response<bool>.Fail(BasketMessages.BasketNotFoundMessage, HttpStatusCode.NotFound);
        }

        public async Task<Shared.Dtos.Response<bool>> CheckoutBasket(BasketCheckoutDto basketCheckoutDto)
        {
            var basket = await _basketRepository.GetAysnc(basketCheckoutDto.UserId);

            if (basket == null)
            {
                return Shared.Dtos.Response<bool>.Fail(BasketMessages.BasketNotFoundMessage, HttpStatusCode.BadRequest);
            }

            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckoutDto);

            eventMessage.TotalPrice = basket.TotalPrice;

            await _publishEndpoint.Publish<IBasketCheckoutEvent>(eventMessage);

            return Shared.Dtos.Response<bool>.Success(HttpStatusCode.OK);
        }


        public async Task<Shared.Dtos.Response<bool>> BasketPriceChangedTest(string userId, string productId, decimal price)
        {
            await _publishEndpoint.Publish<IBasketPriceChangedEvent>(new BasketPriceChangedEvent
            {
                Price = price,
                ProductId = productId,
                UserId = userId
            });
            return Shared.Dtos.Response<bool>.Success(HttpStatusCode.OK);
        }

        private Shared.Dtos.Response<bool> IsEnoughStockAvailable(string productId, int quantity)
        {
            //The current stock was taken from the product data from the product service
            if (quantity > 10)
            {
                return Shared.Dtos.Response<bool>.Fail(BasketMessages.ProductHaveStockMessage, HttpStatusCode.BadRequest);
            }

            return Shared.Dtos.Response<bool>.Success(HttpStatusCode.OK);
        }
    }
}