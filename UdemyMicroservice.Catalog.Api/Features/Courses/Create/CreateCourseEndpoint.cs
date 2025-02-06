using Microsoft.AspNetCore.Mvc;
using UdemyMicroservice.Catalog.Api.Features.Categories.Create;
using UdemyMicroservices.Shared.Filters;
using UdemyMicroservices.Shared.ProduceTypes;

namespace UdemyMicroservice.Catalog.Api.Features.Courses.Create
{
    public static class CreateCourseEndpoint
    {
        public static RouteGroupBuilder CreateCourseGroupItemEndPoint(this RouteGroupBuilder routeGroupBuilder)
        {
            // ( / => base url ) -> http://localhost:5000/api/categories
            routeGroupBuilder.MapPost("/", async (CreateCourseCommand command, IMediator mediator) =>
                (await mediator.Send(command)).ToGenericResult())
                .MapToApiVersion(1, 2)
                .Produces<Guid>(StatusCodes.Status201Created)
                .Produces<NotFoundType>(StatusCodes.Status404NotFound)
                .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
                .AddEndpointFilter<ValidationFilter<CreateCourseCommand>>();

            return routeGroupBuilder;
        }
    }
}
