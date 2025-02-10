using Microsoft.Extensions.FileProviders;
using UdemyMicroservice.File.Api;
using UdemyMicroservice.File.Api.Features;
using UdemyMicroservices.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot")));
builder.Services.AddCommonServiceExt(typeof(FileAssembly));
builder.Services.AddVersionServiceExt();

var app = builder.Build();
app.AddFileGroupEndpointExt(app.AddVersionSetExt());
app.UseStaticFiles();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.Run();

