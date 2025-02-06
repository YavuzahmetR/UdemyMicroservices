namespace UdemyMicroservice.Catalog.Api.Features.Categories.GetAll
{
    public sealed record GetAllQueryRequest : IRequestByServiceResult<List<GetAllQueryResponse>>;

    public sealed class GetAllQueryHandler(AppDbContext dbContext, IMapper mapper) : 
        IRequestHandler<GetAllQueryRequest, ServiceResult<List<GetAllQueryResponse>>>
    {
        public async Task<ServiceResult<List<GetAllQueryResponse>>> Handle(GetAllQueryRequest request, CancellationToken cancellationToken)
        {
            var categories = await dbContext.Categories.ToListAsync(cancellationToken);
            var response = mapper.Map<List<GetAllQueryResponse>>(categories); //Bind the categories to the response
            return ServiceResult<List<GetAllQueryResponse>>.SuccessAsOk(response); //200 OK
        }
    }
    public sealed record GetAllQueryResponse(Guid Id, string Name);

    public static class GetAllCategoriesQueryEndPoint 
    {
        public static RouteGroupBuilder GetAllCategoriesGroupItemEndPoint(this RouteGroupBuilder routeGroupBuilder)
        {
            routeGroupBuilder.MapGet("/", async (IMediator mediator) =>
                (await mediator.Send(new GetAllQueryRequest())).ToGenericResult())
                .MapToApiVersion(1, 0);
            return routeGroupBuilder;
        }
    }
}
