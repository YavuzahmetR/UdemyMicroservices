using MongoDB.Driver;
using System.Reflection;

namespace UdemyMicroservice.Discount.Api.Repositories
{
    public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<UdemyMicroservice.Discount.Api.Features.Discount> Discounts { get; set; } 

        public static AppDbContext Create(IMongoDatabase database)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
                .UseMongoDB(database.Client,database.DatabaseNamespace.DatabaseName);

            return new AppDbContext(optionsBuilder.Options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
