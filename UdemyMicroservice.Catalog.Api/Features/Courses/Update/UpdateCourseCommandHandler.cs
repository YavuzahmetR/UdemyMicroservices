
namespace UdemyMicroservice.Catalog.Api.Features.Courses.Update
{
    public sealed class UpdateCourseCommandHandler(AppDbContext dbContext) : IRequestHandler<UpdateCourseCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var hasCourse = await dbContext.Courses.FindAsync([request.Id], cancellationToken: cancellationToken);

            if (hasCourse is null)
            {
                return ServiceResult.Error("Course not found",
                    $"The course with id({request.Id}) was not found", HttpStatusCode.NotFound);
            }

            hasCourse.Name = request.Name;
            hasCourse.Description = request.Description;
            hasCourse.Price = request.Price;
            hasCourse.ImageUrl = request.ImageUrl;
            hasCourse.CategoryId = request.CategoryId;

            dbContext.Courses.Update(hasCourse);
            await dbContext.SaveChangesAsync(cancellationToken);

            return ServiceResult.SuccessAsNoContent();

        }
    }
}
