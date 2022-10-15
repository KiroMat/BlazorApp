using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Client.Services.Exceptions
{
    public class ApiExeption : Exception
    {
        public ProblemDetails ApiErrorRespone{ get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public ApiExeption(ProblemDetails apiErrorRespone, HttpStatusCode statusCode) : this(apiErrorRespone)
        {
            StatusCode = statusCode;
        }

        public ApiExeption(ProblemDetails apiErrorRespone)
        {
            ApiErrorRespone = apiErrorRespone;
        }
    }
}
