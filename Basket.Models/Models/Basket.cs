using System.Collections.Generic;
using System.Linq;

namespace Basket.Models.Models
{
    public class Basket
    {
        public string UserId { get; set; }

        public string DiscountCode { get; set; }

        public int? DiscountRate { get; set; }

        public List<BasketItem> BasketItems { get; set; }

        public decimal TotalPrice => BasketItems.Sum(item => item.Price * item.Quantity);

        public Basket()
        {
            BasketItems = new List<BasketItem>();
        }
    }
}
