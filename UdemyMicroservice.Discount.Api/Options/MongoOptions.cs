using System.ComponentModel.DataAnnotations;

namespace UdemyMicroservice.Discount.Api.Options
{
    public sealed class MongoOptions
    {
        [Required] public string DatabaseName { get; set; } = default!;
        [Required] public string ConnectionString { get; set; } = default!;
    }
}
