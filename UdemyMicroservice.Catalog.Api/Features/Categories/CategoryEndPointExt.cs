using Asp.Versioning.Builder;
using UdemyMicroservice.Catalog.Api.Features.Categories.Create;
using UdemyMicroservice.Catalog.Api.Features.Categories.GetAll;
using UdemyMicroservice.Catalog.Api.Features.Categories.GetById;

namespace UdemyMicroservice.Catalog.Api.Features.Categories
{
    public static class CategoryEndPointExt
    {
        public static void AddCategoryGroupEndPointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/categories").WithTags("Categories").WithApiVersionSet(apiVersionSet)
                .CreateCategoryGroupItemEndPoint()
                .GetAllCategoriesGroupItemEndPoint()
                .GetByIdCategoryGroupItemEndPoint();
        }
    }
}
