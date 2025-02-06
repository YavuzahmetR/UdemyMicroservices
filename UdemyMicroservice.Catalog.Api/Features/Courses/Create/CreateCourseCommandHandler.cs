
namespace UdemyMicroservice.Catalog.Api.Features.Courses.Create
{
    public class CreateCourseCommandHandler(AppDbContext dbContext, IMapper mapper) : IRequestHandler<CreateCourseCommand, ServiceResult<Guid>>
    {
        public async Task<ServiceResult<Guid>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            bool hasCategory = await dbContext.Categories.AnyAsync(c => c.Id == request.CategoryId, cancellationToken);
            if (!hasCategory)
            {
                return ServiceResult<Guid>.Error("Category not found",
                    $"Category given with the '{request.CategoryId}' Id was not found"
                    , HttpStatusCode.NotFound);
            }

            bool hasCourse = await dbContext.Courses.AnyAsync(c => c.Name == request.Name, cancellationToken);

            if (hasCourse)
            {
                return ServiceResult<Guid>.Error("Course already exists",
                    $"Course with name '{request.Name}' already exists"
                    , HttpStatusCode.BadRequest);
            }

            var newCourse = mapper.Map<Course>(request);
            newCourse.CreatedTime = DateTime.Now;
            newCourse.Feature = new Feature
            {
                Duration = 100, // Algortihm will be implemented
                EducatorFullName = "Ahmet Can",
                Rating = 0
            };

            await dbContext.Courses.AddAsync(newCourse, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return ServiceResult<Guid>.SuccessAsCreated(newCourse.Id, $"/api/courses/{newCourse.Id}");
        }
    }
}
