using System.Net;
using Shared.Dtos;

namespace Basket.Core.Utilities.Business
{
    public class BusinessRules
    {
        public static Response<T> Run<T>(params Response<T>[] logics)
        {
            foreach (var result in logics)
            {
                if (!result.IsSuccessfull)
                {
                    return result;
                }
            }

            return Response<T>.Success(HttpStatusCode.OK);
        }
    }
}
