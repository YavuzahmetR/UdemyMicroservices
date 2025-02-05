using FluentValidation;

namespace UdemyMicroservice.Catalog.Api.Features.Categories.Create
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("name field cannot be empty").
                Length(3, 100).WithMessage("name field must be between 3 - 100 characters");
          
        }
    }
}
