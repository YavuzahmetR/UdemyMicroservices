using Microsoft.AspNetCore.Mvc;
using UdemyMicroservices.Shared.Filters;
using UdemyMicroservices.Shared.ProduceTypes;

namespace UdemyMicroservice.Catalog.Api.Features.Courses.Update
{
    public static class UpdateCourseCommandEndPoint
    {
        public static RouteGroupBuilder UpdateCourseGroupItemEndPoint(this RouteGroupBuilder groupBuilder)
        {
            groupBuilder.MapPut("/", async (UpdateCourseCommand command,IMediator mediator) =>
            (await mediator.Send(command)).ToGenericResult())
                .MapToApiVersion(1, 2)
                .Produces(StatusCodes.Status204NoContent)
                .Produces<NotFoundType>(StatusCodes.Status404NotFound)
                .AddEndpointFilter<ValidationFilter<UpdateCourseCommand>>();
            return groupBuilder;
        }
    }
}
