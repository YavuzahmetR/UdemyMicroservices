using UdemyMicroservice.Catalog.Api.Features.Courses.Create;
using UdemyMicroservice.Catalog.Api.Features.Courses.GetAll;
using UdemyMicroservice.Catalog.Api.Features.Courses.Response;

namespace UdemyMicroservice.Catalog.Api.Features.Courses
{
    public sealed class CourseMapper : Profile
    {
        public CourseMapper()
        {
            CreateMap<Course,CreateCourseCommand>().ReverseMap();
            CreateMap<Course, CourseQueryResponse>().ReverseMap();
        }
    }
}
