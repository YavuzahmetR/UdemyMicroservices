using UdemyMicroservice.Catalog.Api;
using UdemyMicroservice.Catalog.Api.Features.Categories;
using UdemyMicroservice.Catalog.Api.Features.Courses;
using UdemyMicroservice.Catalog.Api.Options;
using UdemyMicroservice.Catalog.Api.Repositories;
using UdemyMicroservices.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptExt();
builder.Services.AddDbServiceExt();
builder.Services.AddCommonServiceExt(typeof(CatalogAssembly));
builder.Services.AddVersionServiceExt();

var app = builder.Build();
app.AddSeedDataExt().ContinueWith(x =>
{
    Console.WriteLine(x.IsFaulted ? x.Exception?.Message : "Seed data has been saved successfully");
});
app.AddCategoryGroupEndPointExt(app.AddVersionSetExt());
app.AddCourseGroupEndPointExt(app.AddVersionSetExt());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.Run();


