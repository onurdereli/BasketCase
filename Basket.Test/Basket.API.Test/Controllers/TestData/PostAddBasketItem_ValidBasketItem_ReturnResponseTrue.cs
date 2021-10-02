using System.Collections;
using System.Collections.Generic;
using Basket.Models.Request;

namespace Basket.Test.Basket.API.Test.Controllers.TestData
{
    public class PostAddBasketItem_ValidBasketItem_ReturnResponseTrue : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "onur", new BasketItemRequest() { ProductId = "1", Price = 20, ProductCode = "AX1", Quantity = 10 } };
            yield return new object[] { "onur", new BasketItemRequest() { ProductId = "2", Price = 20, ProductCode = "AX1", Quantity = 10 } };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
