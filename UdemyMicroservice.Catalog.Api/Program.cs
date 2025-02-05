using UdemyMicroservice.Catalog.Api;
using UdemyMicroservice.Catalog.Api.Features.Categories;
using UdemyMicroservice.Catalog.Api.Options;
using UdemyMicroservice.Catalog.Api.Repositories;
using UdemyMicroservices.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptExt();
builder.Services.AddDbServiceExt();
builder.Services.AddCommonServiceExt(typeof(CatalogAssembly));


var app = builder.Build();

app.AddCategoryGroupEndPointExt();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.Run();


