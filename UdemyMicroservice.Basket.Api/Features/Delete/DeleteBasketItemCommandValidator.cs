using FluentValidation;

namespace UdemyMicroservice.Basket.Api.Features.Delete
{
    public class DeleteBasketItemCommandValidator : AbstractValidator<DeleteBasketItemCommand>
    {
        public DeleteBasketItemCommandValidator()
        {
            RuleFor(x => x.Id)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");
        } 
    }
}
