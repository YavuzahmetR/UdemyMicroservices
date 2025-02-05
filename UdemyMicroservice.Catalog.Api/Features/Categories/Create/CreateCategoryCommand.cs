
using MediatR;
using UdemyMicroservices.Shared;

namespace UdemyMicroservice.Catalog.Api.Features.Categories.Create
{
    public sealed record CreateCategoryCommand(
        string Name) : IRequestByServiceResult<CreateCategoryResponse>;

}
