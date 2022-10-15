using DataApi.Shared.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Client.Services.Exceptions
{
    public class ApiExeption : Exception
    {
        public ErrorApiResponse ApiErrorRespone { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public ApiExeption(ErrorApiResponse apiErrorRespone, HttpStatusCode statusCode) : this(apiErrorRespone)
        {
            StatusCode = statusCode;
        }

        public ApiExeption(ErrorApiResponse apiErrorRespone)
        {
            ApiErrorRespone = apiErrorRespone;
        }
    }
}
