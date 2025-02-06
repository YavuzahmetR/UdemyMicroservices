using UdemyMicroservice.Catalog.Api.Features.Courses.GetAll;
using UdemyMicroservice.Catalog.Api.Features.Courses.Response;
using UdemyMicroservices.Shared.ProduceTypes;

namespace UdemyMicroservice.Catalog.Api.Features.Courses.GetById
{
    public sealed record GetByIdCourseQuery(Guid Id): IRequestByServiceResult<CourseQueryResponse>;

    public sealed class GetByIdCourseQueryHandler(AppDbContext dbContext, IMapper mapper) : IRequestHandler<GetByIdCourseQuery, ServiceResult<CourseQueryResponse>>
    {
        public async Task<ServiceResult<CourseQueryResponse>> Handle(GetByIdCourseQuery request, CancellationToken cancellationToken)
        {
            var hasCourse = await dbContext.Courses.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (hasCourse is null)
            {
                return ServiceResult<CourseQueryResponse>.Error("Course not found",
                    $"The course with id({request.Id}) was not found", HttpStatusCode.NotFound);
            }

            var hasCategory = await dbContext.Categories.FindAsync(hasCourse.CategoryId, cancellationToken);

            if (hasCategory is null)
            {
                return ServiceResult<CourseQueryResponse>.Error("Category not found",
                    $"The category with id({hasCourse.CategoryId}) was not found", HttpStatusCode.NotFound);
            }

            hasCourse.Category = hasCategory;

            var response = mapper.Map<CourseQueryResponse>(hasCourse);

            return ServiceResult<CourseQueryResponse>.SuccessAsOk(response);
        }
    }
    public static class GetByIdCourseQueryEndPoint
    {
        public static RouteGroupBuilder GetByIdCourseGroupItemEndPoint(this RouteGroupBuilder routeGroup)
        {
            routeGroup.MapGet("/{id:guid}", async (IMediator mediator, Guid id) =>
                (await mediator.Send(new GetByIdCourseQuery(id))).ToGenericResult())
                .MapToApiVersion(1, 2)
                .Produces<CourseQueryResponse>(StatusCodes.Status200OK)
                .Produces<NotFoundType>(StatusCodes.Status404NotFound);
            return routeGroup;
        }
    }
}
