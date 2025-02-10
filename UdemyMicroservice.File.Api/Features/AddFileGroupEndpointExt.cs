using Asp.Versioning.Builder;
using UdemyMicroservice.File.Api.Features.Delete;
using UdemyMicroservice.File.Api.Features.Upload;

namespace UdemyMicroservice.File.Api.Features
{
    public static class FileEndpointExt
    {
        public static void AddFileGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/files").WithTags("Files").WithApiVersionSet(apiVersionSet)
                .UploadFileGroupItemEndpoint()
                .DeleteFileGroupItemEndpoint();
        }
    }
}
