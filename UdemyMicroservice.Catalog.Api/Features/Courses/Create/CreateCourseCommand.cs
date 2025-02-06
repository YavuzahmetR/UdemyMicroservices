namespace UdemyMicroservice.Catalog.Api.Features.Courses.Create
{
    public sealed record CreateCourseCommand(
        string Name,
        string Description,
        decimal Price,
        Guid CategoryId,
        string ImageUrl) : IRequestByServiceResult<Guid>;
}
