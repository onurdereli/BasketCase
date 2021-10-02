using System.Collections.Generic;
using System.Linq;

namespace Basket.Models.Dtos
{
    public class BasketDto
    {
        public string UserId { get; set; }

        public string DiscountCode { get; set; }

        public int? DiscountRate { get; set; }

        public List<BasketItemDto> BasketItems { get; set; }

        public decimal TotalPrice => BasketItems.Sum(item => item.Price * item.Quantity);

        public BasketDto()
        {
            BasketItems = new List<BasketItemDto>();
        }
    }
}
