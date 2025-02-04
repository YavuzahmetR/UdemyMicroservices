namespace UdemyMicroservice.Catalog.Api.Features.Courses
{
    public sealed class Feature
    {
        public int Duration { get; set; }
        public float Rating { get; set; }
        public string EducatorFullName { get; set; } = default!;
    }
}
