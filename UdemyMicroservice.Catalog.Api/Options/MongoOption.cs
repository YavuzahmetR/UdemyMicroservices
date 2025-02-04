using System.ComponentModel.DataAnnotations;

namespace UdemyMicroservice.Catalog.Api.Options
{
    public sealed class MongoOption
    {
        [Required]
        public string ConnectionString { get; set; } = default!;
        [Required]
        public string DatabaseName { get; set; } = default!;
    }
}
