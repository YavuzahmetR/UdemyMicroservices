using AutoMapper;
using MediatR;
using System.Net;
using UdemyMicroservice.Catalog.Api.Repositories;
using UdemyMicroservices.Shared;
using UdemyMicroservices.Shared.Extensions;

namespace UdemyMicroservice.Catalog.Api.Features.Categories.GetById
{
    public sealed record GetByIdQueryRequest(Guid Id) : IRequestByServiceResult<GetByIdQueryResponse>;

    public sealed class GetByIdQueryHandler(AppDbContext dbContext, IMapper mapper) : IRequestHandler<GetByIdQueryRequest, ServiceResult<GetByIdQueryResponse>>
    {
        public async Task<ServiceResult<GetByIdQueryResponse>> Handle(GetByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var category = await dbContext.Categories.FindAsync(request.Id);
            if (category == null)
            {
                return ServiceResult<GetByIdQueryResponse>.Error("Category not found",
                    $"Category given with the '{request.Id}' Id was not found"
                    , HttpStatusCode.NotFound);
            }
            var response = mapper.Map<GetByIdQueryResponse>(category);
            return ServiceResult<GetByIdQueryResponse>.SuccessAsOk(response);
        }
    }

    public sealed record GetByIdQueryResponse(Guid Id, string Name);
    public static class GetByIdCategoryQueryEndPoint
    {
        public static RouteGroupBuilder GetByIdCategoryGroupItemEndPoint(this RouteGroupBuilder routeGroupBuilder)
        {
            routeGroupBuilder.MapGet("/{id:guid}", async (IMediator mediator,Guid id) =>
                (await mediator.Send(new GetByIdQueryRequest(id))).ToGenericResult())
                .MapToApiVersion(1, 0);
            return routeGroupBuilder;
        }
    }

}

