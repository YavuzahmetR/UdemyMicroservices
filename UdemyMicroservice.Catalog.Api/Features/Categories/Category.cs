using System.Text.Json.Serialization;
using UdemyMicroservice.Catalog.Api.Features.Courses;
using UdemyMicroservice.Catalog.Api.Repositories;

namespace UdemyMicroservice.Catalog.Api.Features.Categories
{
    public sealed class Category : BaseEntity
    {
        public string Name { get; set; } = default!;
        [JsonIgnore] public List<Course>? Courses { get; set; }
    }
}
