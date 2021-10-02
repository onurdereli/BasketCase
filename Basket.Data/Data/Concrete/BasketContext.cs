using Basket.Data.Data.Abstract;
using StackExchange.Redis;

namespace Basket.Data.Data.Concrete
{
    public class BasketContext : IBasketContext
    {
        public BasketContext(ConnectionMultiplexer redisConnection)
        {
            Redis = redisConnection.GetDatabase();
        }

        public IDatabase Redis { get; }
    }
}