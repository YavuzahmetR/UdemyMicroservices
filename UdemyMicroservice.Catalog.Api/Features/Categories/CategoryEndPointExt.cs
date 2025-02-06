using UdemyMicroservice.Catalog.Api.Features.Categories.Create;
using UdemyMicroservice.Catalog.Api.Features.Categories.GetAll;
using UdemyMicroservice.Catalog.Api.Features.Categories.GetById;

namespace UdemyMicroservice.Catalog.Api.Features.Categories
{
    public static class CategoryEndPointExt
    {
        public static void AddCategoryGroupEndPointExt(this WebApplication app)
        {
            app.MapGroup("api/categories").WithTags("Categories")
                .CreateCategoryGroupItemEndPoint()
                .GetAllCategoryGroupItemEndPoint()
                .GetByIdCategoryGroupItemEndPoint();
        }
    }
}
