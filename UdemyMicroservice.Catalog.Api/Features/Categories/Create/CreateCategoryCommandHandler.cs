using Amazon.Runtime.Internal;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using UdemyMicroservice.Catalog.Api.Repositories;
using UdemyMicroservices.Shared;

namespace UdemyMicroservice.Catalog.Api.Features.Categories.Create
{
    public sealed class CreateCategoryCommandHandler(AppDbContext dbContext) : IRequestHandler<CreateCategoryCommand, ServiceResult<CreateCategoryResponse>>
    {
        public async Task<ServiceResult<CreateCategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            bool existingCategory = await dbContext.Categories.AnyAsync(c => c.Name == request.Name);
            if(existingCategory)
            {
                return ServiceResult<CreateCategoryResponse>.Error(
                    title: "Category already exists",
                    description: $"Category with name '{request.Name}' already exists",
                    status: HttpStatusCode.BadRequest);

            }

            Category newCategory = new Category
            {
                Name = request.Name,
                Id = NewId.NextSequentialGuid()
            };

            await dbContext.Categories.AddAsync(newCategory, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            return ServiceResult<CreateCategoryResponse>.SuccessAsCreated(new CreateCategoryResponse(newCategory.Id), "<empty>");
        }
    }
}
