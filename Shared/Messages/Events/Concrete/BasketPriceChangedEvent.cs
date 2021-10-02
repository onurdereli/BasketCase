using Shared.Messages.Events.Abstract;
using Shared.Messages.Events.Base;

namespace Shared.Messages.Events.Concrete
{
    public class BasketPriceChangedEvent : BaseEvent, IBasketPriceChangedEvent
    {
        public string ProductId { get; set; }

        public string UserId { get; set; }

        public decimal Price { get; set; }
    }
}
