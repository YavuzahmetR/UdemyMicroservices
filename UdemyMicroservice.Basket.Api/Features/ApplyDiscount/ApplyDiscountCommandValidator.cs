using FluentValidation;

namespace UdemyMicroservice.Basket.Api.Features.ApplyDiscount

{
    public sealed class ApplyDiscountCodeCommandValidator : AbstractValidator<ApplyDiscountCodeCommand>
    {
        public ApplyDiscountCodeCommandValidator()
        {
            RuleFor(x => x.DiscountCode).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.DiscountRate).GreaterThan(0).WithMessage("{PropertyName} must be greater than zero");
        }
       
    }

}
