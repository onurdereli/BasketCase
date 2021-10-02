using System.Collections;
using System.Collections.Generic;

namespace Basket.Test.Basket.API.Test.Controllers.TestData
{
    public class PutUpdateBasketItem_NotEnoughStock_ReturnBadRequest : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "onur", "1", "11" };
            yield return new object[] { "onur", "2", "110" };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
