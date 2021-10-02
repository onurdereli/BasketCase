using System.Threading.Tasks;
using Basket.Models.Dtos;

namespace Basket.Data.Repository.Abstract
{
    public interface IBasketRepository
    {
        Task<BasketDto> GetAysnc(string userId);

        Task<bool> SaveOrUpdateAsyc(Models.Models.Basket basket);

        Task<bool> DeleteAysnc(string userId);
    }
}
