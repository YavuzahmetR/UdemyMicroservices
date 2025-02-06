
using UdemyMicroservices.Shared.ProduceTypes;

namespace UdemyMicroservice.Catalog.Api.Features.Courses.Delete
{
    public sealed record DeleteCourseCommand(Guid Id) : IRequestByServiceResult;


    public sealed class DeleteCourseEndpointHandler(AppDbContext dbContext) : IRequestHandler<DeleteCourseCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var hasCourse = await dbContext.Courses.FindAsync([request.Id], cancellationToken);
            if (hasCourse is null)
            {
                return ServiceResult.ErrorAsNotFound();
            }
            dbContext.Courses.Remove(hasCourse);
            await dbContext.SaveChangesAsync(cancellationToken);
            return ServiceResult.SuccessAsNoContent();
        }
    }

    public static class DeleteCourseEndpoint
    {
        public static RouteGroupBuilder DeleteCourseGroupItemEndPoint(this RouteGroupBuilder routeGroup)
        {
            routeGroup.MapDelete("/{id:guid}", async (IMediator mediator, Guid id) =>
                (await mediator.Send(new DeleteCourseCommand(id))).ToGenericResult())
                .MapToApiVersion(1, 2)
                .Produces<NotFoundType>(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status204NoContent);
            return routeGroup;
        }
    }
}
