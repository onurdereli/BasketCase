using System.Linq;
using System.Threading.Tasks;
using Basket.Services.Abstract;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Messages.Events.Abstract;

namespace Basket.API.Consumers
{
    public class BasketPriceChangedEventConsumer : IConsumer<IBasketPriceChangedEvent>
    {
        private readonly IBasketService _basketService;
        private readonly ILogger<BasketPriceChangedEventConsumer> _logger;

        public BasketPriceChangedEventConsumer(IBasketService basketService, ILogger<BasketPriceChangedEventConsumer> logger)
        {
            _basketService = basketService;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<IBasketPriceChangedEvent> context)
        {
            var basket = await _basketService.GetBasketAysnc(context.Message.UserId);

            var basketItem = basket.Data.BasketItems.Where(item => item.ProductId == context.Message.ProductId).ToList();

            basketItem.ForEach(item => item.Price = context.Message.Price);

            await _basketService.SaveOrUpdateAsync(basket.Data);

            _logger.LogInformation("Price is changed");
        }
    }
}
