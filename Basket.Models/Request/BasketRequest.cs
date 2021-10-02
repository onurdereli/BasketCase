using System.Collections.Generic;
using System.Linq;

namespace Basket.Models.Request
{
    public class BasketRequest
    {
        public string UserId { get; set; }

        public string DiscountCode { get; set; }

        public int? DiscountRate { get; set; }

        public List<BasketItemRequest> BasketItems { get; set; }

        public decimal TotalPrice => BasketItems.Sum(item => item.Price * item.Quantity);
    }
}
