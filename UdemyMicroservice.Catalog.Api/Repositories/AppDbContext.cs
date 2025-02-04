using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Reflection;
using UdemyMicroservice.Catalog.Api.Features.Categories;
using UdemyMicroservice.Catalog.Api.Features.Courses;

namespace UdemyMicroservice.Catalog.Api.Repositories
{
    public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Course> Courses { get; set; } 
        public DbSet<Category> Categories { get; set; }


        public static AppDbContext Create(IMongoDatabase database) //This method is used to create a new instance of AppDbContext - MongoDB
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
