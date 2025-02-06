using UdemyMicroservice.Catalog.Api.Features.Categories;

namespace UdemyMicroservice.Catalog.Api.Features.Courses.Response
{
    public sealed record CourseQueryResponse(
        Guid Id,
        string Name,
        string Description,
        decimal Price,
        Guid UserId,
        string? ImageUrl,
        DateTime CreatedTime,
        Category Category,
        Feature Feature);
}
