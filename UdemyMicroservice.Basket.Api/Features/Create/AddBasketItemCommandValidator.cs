using FluentValidation;

namespace UdemyMicroservice.Basket.Api.Features
{
    public sealed class AddBasketItemCommandValidator : AbstractValidator<AddBasketItemCommand>
    {
        public AddBasketItemCommandValidator()
        {
            RuleFor(x => x.CourseId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");
            RuleFor(x => x.CourseName).NotEmpty().WithMessage("CourseName is required");
            RuleFor(x => x.CoursePrice).GreaterThan(0).WithMessage("{PropertyName} must be greater than zero");
        }
    }
}
