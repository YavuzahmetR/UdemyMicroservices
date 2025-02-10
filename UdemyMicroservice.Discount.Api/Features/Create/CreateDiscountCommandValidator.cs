namespace UdemyMicroservice.Discount.Api.Features.Create
{
    public sealed class CreateDiscountCommandValidator : AbstractValidator<CreateDiscountCommand>
    {
        public CreateDiscountCommandValidator()
        {
            RuleFor(x => x.DiscountCode).NotEmpty().WithMessage("{PropertyName} is required.").Length(10).WithMessage("{propertyName} must be 10 characters long");
            RuleFor(x => x.DiscountRate).NotEmpty().WithMessage("{PropertyName} is required.").GreaterThan(0).WithMessage("{PropertyName} must be greater than Zero '0'");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("{PropertyName} is required.").NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");
            RuleFor(x => x.Expired).NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
