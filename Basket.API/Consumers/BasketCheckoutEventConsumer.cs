using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Messages.Events.Abstract;

namespace Basket.API.Consumers
{
    public class BasketCheckoutEventConsumer : IConsumer<IBasketCheckoutEvent>
    {
        private readonly ILogger<BasketCheckoutEventConsumer> _logger;

        public BasketCheckoutEventConsumer(ILogger<BasketCheckoutEventConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<IBasketCheckoutEvent> context)
        {
            _logger.LogInformation("Added for testing userId: {userId}", context.Message.UserId);

            return Task.CompletedTask;
        }
    }
}
