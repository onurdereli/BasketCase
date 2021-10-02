using System.Text.Json;
using System.Threading.Tasks;
using Basket.Data.Data.Abstract;
using Basket.Data.Repository.Abstract;
using Basket.Models.Dtos;

namespace Basket.Data.Repository.Concrete
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IBasketContext _context;

        public BasketRepository(IBasketContext context)
        {
            _context = context;
        }

        public async Task<BasketDto> GetAysnc(string userId)
        {
            var basket = await _context.Redis.StringGetAsync(userId);

            if (basket.IsNullOrEmpty)
            {
                return null;
            }

            return JsonSerializer.Deserialize<BasketDto>(basket);
        }

        public async Task<bool> SaveOrUpdateAsyc(Models.Models.Basket basket)
        {
            return await _context.Redis.StringSetAsync(basket.UserId, JsonSerializer.Serialize(basket));
        }

        public async Task<bool> DeleteAysnc(string userId)
        {
            return await _context.Redis.KeyDeleteAsync(userId);
        }
    }
}
