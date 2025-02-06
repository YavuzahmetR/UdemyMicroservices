using MediatR;
using Microsoft.AspNetCore.Mvc;
using UdemyMicroservices.Shared.Extensions;
using UdemyMicroservices.Shared.Filters;
using UdemyMicroservices.Shared.ProduceTypes;

namespace UdemyMicroservice.Catalog.Api.Features.Categories.Create
{
    public static class CreateCategoryEndPoint
    {
        public static RouteGroupBuilder CreateCategoryGroupItemEndPoint(this RouteGroupBuilder routeGroupBuilder)
        {
            // ( / => base url ) -> http://localhost:5000/api/categories
            routeGroupBuilder.MapPost("/", async (CreateCategoryCommand command, IMediator mediator) =>
                (await mediator.Send(command)).ToGenericResult())
                .Produces<CreateCategoryResponse>(StatusCodes.Status201Created)
                .Produces<NotFoundType>(StatusCodes.Status404NotFound)
                .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
                .AddEndpointFilter<ValidationFilter<CreateCategoryCommand>>();

            return routeGroupBuilder;
        }
    }
}
