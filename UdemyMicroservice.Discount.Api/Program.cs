using UdemyMicroservice.Catalog.Api;
using UdemyMicroservice.Discount.Api.Features;
using UdemyMicroservice.Discount.Api.Options;
using UdemyMicroservice.Discount.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptionsExt();
builder.Services.AddDatabaseServiceExt();
builder.Services.AddCommonServiceExt(typeof(DiscountAssembly));
builder.Services.AddVersionServiceExt();

var app = builder.Build();
app.AddDiscountGroupEndpointExt(app.AddVersionSetExt());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.Run();

