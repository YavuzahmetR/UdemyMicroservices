using UdemyMicroservice.Discount.Api.Repositories;
using UdemyMicroservices.Shared.Services;

namespace UdemyMicroservice.Discount.Api.Features.Create
{
    public sealed class CreateDiscountCommandHandler(AppDbContext context, IIdentiyService identityService) : IRequestHandler<CreateDiscountCommand, ServiceResult<Guid>>
    {
        public async Task<ServiceResult<Guid>> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var hasCodeForUser = await context.Discounts.AnyAsync(x => x.UserId == identityService.UserId && x.DiscountCode == request.DiscountCode, cancellationToken);

            if (hasCodeForUser)
            {
                return ServiceResult<Guid>.Error("Discount Code Already Exists","Discount code already exists for this user", HttpStatusCode.BadRequest);
            }

            var newDiscount = new Discount
            {
                DiscountCode = request.DiscountCode,
                DiscountRate = request.DiscountRate,
                UserId = identityService.UserId,
                Created = DateTime.Now,
                Expired = request.Expired
            };

            await context.Discounts.AddAsync(newDiscount, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult<Guid>.SuccessAsCreated(newDiscount.Id, $"/api/discounts/{newDiscount.Id}");
        }
    }
}
