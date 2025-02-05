using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;


namespace UdemyMicroservices.Shared.Filters
{
    public sealed class ValidationFilter<T> : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();
            //Fast Fail
            if (validator is null)
            {
                return await next(context);
            }
            var requestModel = context.Arguments.OfType<T>().FirstOrDefault(); //Get the first argument of type T in the request model

            if (requestModel is null)
            {
                return await next(context);
            }

            var validationResult = await validator.ValidateAsync(requestModel);

            if (!validationResult.IsValid)
            {
               return Results.ValidationProblem(validationResult.ToDictionary());
            }

            // This is the place where we can validate the incoming request - Do something before the request is processed
            return await next(context);
        }
    }
}
