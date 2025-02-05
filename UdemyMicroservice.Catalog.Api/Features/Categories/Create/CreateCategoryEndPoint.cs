using MediatR;
using Microsoft.AspNetCore.Mvc;
using UdemyMicroservices.Shared.Extensions;
using UdemyMicroservices.Shared.Filters;

namespace UdemyMicroservice.Catalog.Api.Features.Categories.Create
{
    public static class CreateCategoryEndPoint
    {
        public static RouteGroupBuilder CreateCategoryGroupItemEndPoint(this RouteGroupBuilder routeGroupBuilder)
        {
            // ( / => base url ) -> http://localhost:5000/api/categories
            routeGroupBuilder.MapPost("/", async (CreateCategoryCommand command, IMediator mediator) =>
                (await mediator.Send(command)).ToGenericResult()).AddEndpointFilter<ValidationFilter<CreateCategoryCommand>>();

            return routeGroupBuilder;
        }
    }
}
