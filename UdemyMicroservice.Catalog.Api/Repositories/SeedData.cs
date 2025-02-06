using UdemyMicroservice.Catalog.Api.Features.Categories;
using UdemyMicroservice.Catalog.Api.Features.Courses;

namespace UdemyMicroservice.Catalog.Api.Repositories
{
    public static class SeedData
    {
        public static async Task AddSeedDataExt(this WebApplication app)
        {
            using IServiceScope scope = app.Services.CreateScope();
            CancellationToken cancellationToken = scope.ServiceProvider.GetRequiredService<IHostApplicationLifetime>().ApplicationStopping;

            AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            context.Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;

            if (!await context.Categories.AnyAsync())
            {
                var categories = new List<Category>
                    {
                        new Category { Name = "IT & Software Development" },
                        new Category { Name = "Business" },
                        new Category { Name = "Office Productivity" },
                        new Category { Name = "Personal Development" },
                        new Category { Name = "Design" },
                        new Category { Name = "Marketing" },
                        new Category { Name = "Lifestyle" },
                        new Category { Name = "Photography" },
                        new Category { Name = "Health & Fitness" },
                        new Category { Name = "Music" },
                        new Category { Name = "Teaching & Academics" }
                    };
                context.Categories.AddRange(categories);
                await context.SaveChangesAsync(cancellationToken);
            }

            if (!await context.Courses.AnyAsync())
            {
                Category categories = await context.Categories.FirstAsync();
                Guid randomUserId = NewId.NextGuid();
                List<Course> courses = new List<Course>
                    {
                        new()  { Id = NewId.NextSequentialGuid(), Name = "C# Fundamentals", Description = "Learn C# Fundamentals", Price = 100, CreatedTime = DateTime.UtcNow ,CategoryId = categories.Id, UserId = randomUserId, Feature = new Feature { Duration = 10, Rating = 4, EducatorFullName = "Ahmet Yıldız" }},
                        new()  { Id = NewId.NextSequentialGuid(), Name = "ASP.NET Core Fundamentals", Description = "Learn ASP.NET Core Fundamentals", Price = 150, CreatedTime = DateTime.UtcNow ,CategoryId = categories.Id, UserId = randomUserId, Feature = new Feature { Duration = 10, Rating = 4, EducatorFullName = "Ahmet Yıldız" } },
                        new()  { Id = NewId.NextSequentialGuid(), Name = "Angular Fundamentals", Description = "Learn Angular Fundamentals", Price = 200, CreatedTime = DateTime.UtcNow , CategoryId = categories.Id, UserId = randomUserId,Feature = new Feature { Duration = 10, Rating = 4, EducatorFullName = "Ahmet Yıldız" } },
                        new()  { Id = NewId.NextSequentialGuid(), Name = "SQL Fundamentals", Description = "Learn SQL Fundamentals", Price = 50, CreatedTime = DateTime.UtcNow ,CategoryId = categories.Id, UserId = randomUserId,Feature = new Feature { Duration = 10, Rating = 4, EducatorFullName = "Ahmet Yıldız" } },
                        new()  { Id = NewId.NextSequentialGuid(), Name = "Python Fundamentals", Description = "Learn Python Fundamentals", Price = 75, CreatedTime = DateTime.UtcNow ,CategoryId = categories.Id, UserId = randomUserId,Feature = new Feature { Duration = 10, Rating = 4, EducatorFullName = "Ahmet Yıldız" } },
                        new()  { Id = NewId.NextSequentialGuid(), Name = "Java Fundamentals", Description = "Learn Java Fundamentals", Price = 125, CreatedTime = DateTime.UtcNow ,CategoryId = categories.Id, UserId = randomUserId,Feature = new Feature { Duration = 10, Rating = 4, EducatorFullName = "Ahmet Yıldız" } },
                        new()  { Id = NewId.NextSequentialGuid(), Name = "React Fundamentals", Description = "Learn React Fundamentals", Price = 175, CreatedTime = DateTime.UtcNow ,CategoryId = categories.Id, UserId = randomUserId,Feature = new Feature { Duration = 10, Rating = 4, EducatorFullName = "Ahmet Yıldız" } },
                        new()  { Id = NewId.NextSequentialGuid(), Name = "Vue Fundamentals", Description = "Learn Vue Fundamentals", Price = 225, CreatedTime = DateTime.UtcNow ,CategoryId = categories.Id, UserId = randomUserId,Feature = new Feature { Duration = 10, Rating = 4, EducatorFullName = "Ahmet Yıldız" } },
                        new()  { Id = NewId.NextSequentialGuid(), Name = "Docker Fundamentals", Description = "Learn Docker Fundamentals", Price = 75, CreatedTime = DateTime.UtcNow ,CategoryId = categories.Id, UserId = randomUserId,Feature = new Feature { Duration = 10, Rating = 4, EducatorFullName = "Ahmet Yıldız" } },
                        new()  { Id = NewId.NextSequentialGuid(), Name = "Kubernetes Fundamentals", Description = "Learn Kubernetes Fundamentals", Price = 100, CreatedTime = DateTime.UtcNow ,CategoryId = categories.Id, UserId = randomUserId,Feature = new Feature { Duration = 10, Rating = 4, EducatorFullName = "Ahmet Yıldız" } },
                        new()  { Id = NewId.NextSequentialGuid(), Name = "Machine Learning Fundamentals", Description = "Learn Machine Learning Fundamentals", Price = 150, CreatedTime = DateTime.UtcNow ,CategoryId = categories.Id, UserId = randomUserId,Feature = new Feature { Duration = 10, Rating = 4, EducatorFullName = "Ahmet Yıldız" } },
                        new()  { Id = NewId.NextSequentialGuid(), Name = "Deep Learning Fundamentals", Description = "Learn Deep Learning Fundamentals", Price = 200, CreatedTime = DateTime.UtcNow ,CategoryId = categories.Id, UserId = randomUserId,Feature = new Feature { Duration = 10, Rating = 4, EducatorFullName = "Ahmet Yıldız" } },
                        new()  { Id = NewId.NextSequentialGuid(), Name = "Artificial Intelligence Fundamentals", Description = "Learn Artificial Intelligence Fundamentals", Price = 250, CreatedTime = DateTime.UtcNow ,CategoryId = categories.Id, UserId = randomUserId,Feature = new Feature { Duration = 10, Rating = 4, EducatorFullName = "Ahmet Yıldız" } },
                        new()  { Id = NewId.NextSequentialGuid(), Name = "Blockchain Fundamentals", Description = "Learn Blockchain Fundamentals", Price = 100 , CreatedTime = DateTime.UtcNow ,CategoryId = categories.Id, UserId = randomUserId,Feature = new Feature { Duration = 10, Rating = 4, EducatorFullName = "Ahmet Yıldız" } }
                    };
                await context.Courses.AddRangeAsync(courses, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
