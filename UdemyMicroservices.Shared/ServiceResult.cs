using Microsoft.AspNetCore.Mvc;
using Refit;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;
namespace UdemyMicroservices.Shared
{
    //Refit
    //ProblemDetails - RFC 7807
    public class ServiceResult
    {
        [JsonIgnore] public HttpStatusCode StatusCode { get; set; }

        public ProblemDetails? Fail { get; set; }

        [JsonIgnore] public bool IsSuccess => Fail is null;
        [JsonIgnore] public bool IsFail => !IsSuccess;

        //static factory methods

        public static ServiceResult SuccessAsNoContent() //204
        {
            return new ServiceResult
            {
                StatusCode = HttpStatusCode.NoContent
            };

        }   
        
        public static ServiceResult ErrorAsNotFound() //404
        {
            return new ServiceResult
            {
                StatusCode = HttpStatusCode.NotFound,
                Fail = new ProblemDetails
                {
                    Title = "Not Found",
                    Detail = "The requested resource was not found"
                }
            };
        }

        public static ServiceResult Error(ProblemDetails problemDetails, HttpStatusCode statusCode)
        {
            return new ServiceResult
            {
                StatusCode = statusCode,
                Fail = problemDetails
            };
        }

        public static ServiceResult Error(string title, string description, HttpStatusCode status)
        {
            return new ServiceResult
            {
                StatusCode = status,
                Fail = new ProblemDetails()
                {
                    Title = title,
                    Detail = description,
                    Status = (int)status
                }
            };
        }

        public static ServiceResult ErrorFromProblemDetails(ApiException apiException)
        {
            if(string.IsNullOrEmpty(apiException.Content))
            {
                return new ServiceResult
                {
                    Fail = new ProblemDetails()
                    {
                        Title = apiException.Message
                    },
                    StatusCode = apiException.StatusCode
                };
            }

            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(apiException.Content,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            return new ServiceResult
            {
                Fail = problemDetails,
                StatusCode = apiException.StatusCode
            };

        }

        public static ServiceResult ErrorFromValidatons(IDictionary<string, object?> errors)
        {
            return new ServiceResult
            {
                StatusCode = HttpStatusCode.BadRequest,
                Fail = new ProblemDetails
                {
                    Title = "Validation errors occured",
                    Detail = "Please check the errors property for more details",
                    Extensions = errors,
                    Status = (int)HttpStatusCode.BadRequest
                }
            };
        }    

    }
    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; set; }

        [JsonIgnore] public string? UrlAsCreated { get; set; }


        public static ServiceResult<T> Success(T data) //200
        {
            return new ServiceResult<T>
            {
                Data = data,
                StatusCode = HttpStatusCode.OK
            };
        }

        //201 => Created => respones body header => location== api/products/5

        public static ServiceResult<T> SuccessAsCreated(T data, string url) //201
        {
            return new ServiceResult<T>
            {
                Data = data,
                StatusCode = HttpStatusCode.Created,
                UrlAsCreated = url
            };
        }


        public new static ServiceResult<T> Error(ProblemDetails problemDetails, HttpStatusCode statusCode)
        {
            return new ServiceResult<T>
            {
                StatusCode = statusCode,
                Fail = problemDetails
            };
        }

        public new static ServiceResult<T> Error(string title, string description, HttpStatusCode status)
        {
            return new ServiceResult<T>
            {
                StatusCode = status,
                Fail = new ProblemDetails()
                {
                    Title = title,
                    Detail = description,
                    Status = (int)status
                }
            };
        }

        public new static ServiceResult<T> ErrorFromProblemDetails(ApiException apiException)
        {
            if (string.IsNullOrEmpty(apiException.Content))
            {
                return new ServiceResult<T>
                {
                    Fail = new ProblemDetails()
                    {
                        Title = apiException.Message
                    },
                    StatusCode = apiException.StatusCode
                };
            }

            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(apiException.Content,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            return new ServiceResult<T>
            {
                Fail = problemDetails,
                StatusCode = apiException.StatusCode
            };

        }

        public new static ServiceResult<T> ErrorFromValidatons(IDictionary<string, object?> errors)
        {
            return new ServiceResult<T>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Fail = new ProblemDetails
                {
                    Title = "Validation errors occured",
                    Detail = "Please check the errors property for more details",
                    Extensions = errors,
                    Status = (int)HttpStatusCode.BadRequest
                }
            };
        }


    }
}
