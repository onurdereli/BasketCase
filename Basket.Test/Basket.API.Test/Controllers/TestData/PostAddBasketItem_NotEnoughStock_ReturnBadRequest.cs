using System.Collections;
using System.Collections.Generic;
using Basket.Models.Request;

namespace Basket.Test.Basket.API.Test.Controllers.TestData
{
    public class PostAddBasketItem_NotEnoughStock_ReturnBadRequest : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "onur", new BasketItemRequest() { ProductId = "1", Price = 20, ProductCode = "AX1", Quantity = 100 } };
            yield return new object[] { "onur", new BasketItemRequest() { ProductId = "2", Price = 20, ProductCode = "AX1", Quantity = 11 } };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
