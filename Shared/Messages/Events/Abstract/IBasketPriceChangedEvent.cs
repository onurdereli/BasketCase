using Shared.Messages.Events.Base;

namespace Shared.Messages.Events.Abstract
{
    public interface IBasketPriceChangedEvent : IBaseEvent
    {
        public string ProductId { get; set; }

        public string UserId { get; set; }

        public decimal Price { get; set; }
    }
}
