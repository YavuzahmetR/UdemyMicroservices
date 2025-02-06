using Asp.Versioning.Builder;
using UdemyMicroservice.Catalog.Api.Features.Courses.Create;
using UdemyMicroservice.Catalog.Api.Features.Courses.Delete;
using UdemyMicroservice.Catalog.Api.Features.Courses.GetAll;
using UdemyMicroservice.Catalog.Api.Features.Courses.GetById;
using UdemyMicroservice.Catalog.Api.Features.Courses.GetByUserId;
using UdemyMicroservice.Catalog.Api.Features.Courses.Update;

namespace UdemyMicroservice.Catalog.Api.Features.Courses
{
    public static class CourseEndPointExt
    {
        public static void AddCourseGroupEndPointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/courses").WithTags("Courses").WithApiVersionSet(apiVersionSet)
               .CreateCourseGroupItemEndPoint()
                .UpdateCourseGroupItemEndPoint()
                .DeleteCourseGroupItemEndPoint()
                .GetAllCourseGroupItemEndpoint()
                .GetByIdCourseGroupItemEndPoint()
                .GetByUserIdCourseGroupItemEndpoint();

        }
    }
}
