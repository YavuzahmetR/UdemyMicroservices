using Microsoft.AspNetCore.Http;
using System.Net;

namespace UdemyMicroservices.Shared.Extensions
{
    public static class EndPointResultExt
    {
        public static IResult ToGenericResult<T>(this ServiceResult<T> result) //Generic Version
        {
            return result.StatusCode switch
            {
                HttpStatusCode.OK => Results.Ok(result.Data),
                HttpStatusCode.Created => Results.Created(result.UrlAsCreated,result.Data),
                HttpStatusCode.NoContent => Results.NoContent(),
                HttpStatusCode.BadRequest => Results.BadRequest(result.Fail),
                HttpStatusCode.NotFound => Results.NotFound(result.Fail),
                _ => Results.Problem(result.Fail!) //500 or other
            };
        }
        public static IResult ToGenericResult(this ServiceResult result) //No Generic Version
        {
            return result.StatusCode switch
            {
                HttpStatusCode.NoContent => Results.NoContent(),
                HttpStatusCode.BadRequest => Results.BadRequest(result.Fail),
                HttpStatusCode.NotFound => Results.NotFound(result.Fail),
                _ => Results.Problem(result.Fail!) //500 or other
            };
        }
    }
}
