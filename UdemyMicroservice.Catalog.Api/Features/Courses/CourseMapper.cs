using UdemyMicroservice.Catalog.Api.Features.Courses.Create;

namespace UdemyMicroservice.Catalog.Api.Features.Courses
{
    public sealed class CourseMapper : Profile
    {
        public CourseMapper()
        {
            CreateMap<Course,CreateCourseCommand>().ReverseMap();
        }
    }
}
