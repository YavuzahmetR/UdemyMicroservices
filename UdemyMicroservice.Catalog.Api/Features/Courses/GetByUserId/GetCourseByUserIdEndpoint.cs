using UdemyMicroservice.Catalog.Api.Features.Courses.Response;

namespace UdemyMicroservice.Catalog.Api.Features.Courses.GetByUserId
{
    public sealed record GetCourseByUserIdQuery(Guid UserId) : IRequestByServiceResult<List<CourseQueryResponse>>;


    public sealed class GetCourseByUserIdQueryHandler(AppDbContext context, IMapper mapper)
        : IRequestHandler<GetCourseByUserIdQuery, ServiceResult<List<CourseQueryResponse>>>
    {
        public async Task<ServiceResult<List<CourseQueryResponse>>> Handle(GetCourseByUserIdQuery request,
            CancellationToken cancellationToken)
        {
            var courses = await context.Courses.Where(x => x.UserId == request.UserId)
                .ToListAsync(cancellationToken);

            var categories = await context.Categories.ToListAsync(cancellationToken: cancellationToken);


            foreach (var course in courses)
            {
                course.Category = categories.First(x => x.Id == course.CategoryId);
            }

            var coursesAsDto = mapper.Map<List<CourseQueryResponse>>(courses);
            return ServiceResult<List<CourseQueryResponse>>.SuccessAsOk(coursesAsDto);
        }
    }

    public static class GetCourseByUserIdEndpoint
    {
        public static RouteGroupBuilder GetByUserIdCourseGroupItemEndpoint(this RouteGroupBuilder groupBuilder)
        {
            groupBuilder.MapGet("/user/{userId:guid}",
                    async (IMediator mediator, Guid userId) =>
                        (await mediator.Send(new GetCourseByUserIdQuery(userId))).ToGenericResult())
                .MapToApiVersion(1, 2);
            return groupBuilder;
        }
    }
}