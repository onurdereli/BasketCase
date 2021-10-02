using StackExchange.Redis;

namespace Basket.Data.Data.Abstract
{
    public interface IBasketContext
    {
        IDatabase Redis { get; } 
    }
}
