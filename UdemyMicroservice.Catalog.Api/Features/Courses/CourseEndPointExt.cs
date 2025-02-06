using UdemyMicroservice.Catalog.Api.Features.Courses.Create;

namespace UdemyMicroservice.Catalog.Api.Features.Courses
{
    public static class CourseEndPointExt
    {
        public static void AddCourseGroupEndPointExt(this WebApplication app)
        {
            app.MapGroup("api/courses").WithTags("Courses")
                .CreateCourseGroupItemEndPoint();

        }
    }
}
