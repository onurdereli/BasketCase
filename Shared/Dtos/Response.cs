using System.Collections.Generic;
using System.Net;
using System.Text.Json.Serialization;

namespace Shared.Dtos
{
    public class Response<T>
    {
        public T Data { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }

        [JsonIgnore]
        public bool IsSuccessfull { get; set; }

        public List<string> Errors { get; set; }

        //Static Factory Methods
        public static Response<T> Success(T data, HttpStatusCode statusCode) => new Response<T> { Data = data, StatusCode = (int) statusCode, IsSuccessfull = true };

        public static Response<T> Success(HttpStatusCode statusCode) => new Response<T> { Data = default(T), StatusCode = (int) statusCode, IsSuccessfull = true };

        public static Response<T> Fail(List<string> errors, HttpStatusCode statusCode) => new Response<T> { Errors = errors, StatusCode = (int)statusCode, IsSuccessfull = false, Data = default };

        public static Response<T> Fail(string error, HttpStatusCode statusCode) => new Response<T> { Errors = new List<string> { error }, StatusCode = (int) statusCode, IsSuccessfull = false };
    }
}
