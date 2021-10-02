using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Models.Request
{
    public class BasketItemRequest
    {
        public string ProductId { get; set; }

        public string ProductCode { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
