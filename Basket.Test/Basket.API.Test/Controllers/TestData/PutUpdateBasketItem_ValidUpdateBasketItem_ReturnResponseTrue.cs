using System.Collections;
using System.Collections.Generic;

namespace Basket.Test.Basket.API.Test.Controllers.TestData
{
    class PutUpdateBasketItem_ValidUpdateBasketItem_ReturnResponseTrue : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "onur", "1", 10 };
            yield return new object[] { "onur", "2", 8 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
