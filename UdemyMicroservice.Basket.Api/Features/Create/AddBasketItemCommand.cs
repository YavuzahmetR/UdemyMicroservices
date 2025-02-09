using MediatR;
using UdemyMicroservices.Shared;

namespace UdemyMicroservice.Basket.Api.Features
{
    public sealed record AddBasketItemCommand(
        Guid CourseId, string CourseName,string? ImageUrl,decimal CoursePrice) : IRequestByServiceResult;
}
