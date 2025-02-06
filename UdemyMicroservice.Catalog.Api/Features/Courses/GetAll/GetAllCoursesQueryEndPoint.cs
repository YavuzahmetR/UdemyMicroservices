using UdemyMicroservice.Catalog.Api.Features.Categories;
using UdemyMicroservice.Catalog.Api.Features.Courses.Response;

namespace UdemyMicroservice.Catalog.Api.Features.Courses.GetAll
{
    public sealed record GetAllCoursesQuery : IRequestByServiceResult<List<CourseQueryResponse>>;
    

    public sealed class GetAllCoursesQueryHandler(AppDbContext dbContext, IMapper mapper) : IRequestHandler<GetAllCoursesQuery, ServiceResult<List<CourseQueryResponse>>>
    {
        public async Task<ServiceResult<List<CourseQueryResponse>>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            var courses = await dbContext.Courses.ToListAsync(cancellationToken);
            var categories = await dbContext.Categories.ToListAsync(cancellationToken); //Get all categories for only one query outside the loop
            //Cannot Use Include() method because of the NOSQL database
            
            foreach (var course in courses)
            {
                course.Category = categories.First(x => x.Id == course.CategoryId);
            }

            var complexCourses = mapper.Map<List<CourseQueryResponse>>(courses);
            return ServiceResult<List<CourseQueryResponse>>.SuccessAsOk(complexCourses);
        }
    }


    public static class GetAllCoursesEndPoint
    {
        public static RouteGroupBuilder GetAllCourseGroupItemEndpoint(this RouteGroupBuilder routeGroupBuilder)
        {
            routeGroupBuilder.MapGet("/", async (IMediator mediator) =>
                (await mediator.Send(new GetAllCoursesQuery())).ToGenericResult())
                .MapToApiVersion(1, 2)
                .Produces<CourseQueryResponse>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError);
            return routeGroupBuilder;
        }
    }




}
