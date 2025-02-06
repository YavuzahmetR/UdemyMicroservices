namespace UdemyMicroservice.Catalog.Api.Features.Courses.Update
{
    public sealed record UpdateCourseCommand(
        Guid Id,
        string Name,
        string Description,
        decimal Price,
        string? ImageUrl,
        Guid CategoryId) : IRequestByServiceResult;

}
