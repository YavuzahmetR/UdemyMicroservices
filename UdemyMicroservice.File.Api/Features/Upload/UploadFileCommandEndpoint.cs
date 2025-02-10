using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Net;
using UdemyMicroservices.Shared;
using UdemyMicroservices.Shared.Extensions;
using UdemyMicroservices.Shared.Filters;
using UdemyMicroservices.Shared.ProduceTypes;

namespace UdemyMicroservice.File.Api.Features.Upload
{
    public sealed record UploadFileCommand(IFormFile File) : IRequestByServiceResult<UploadFileCommandResponse>;

    public sealed record UploadFileCommandResponse(string Filename, string FilePath, string OriginalFileName);

    public sealed class UploadFileCommandHandler(IFileProvider fileProvider) : IRequestHandler<UploadFileCommand, ServiceResult<UploadFileCommandResponse>>
    {
        public async Task<ServiceResult<UploadFileCommandResponse>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            if (request.File.Length == 0)
            {
                return ServiceResult<UploadFileCommandResponse>.Error("Invalid File", "File is empty", HttpStatusCode.BadRequest);
            }

            var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(request.File.FileName)}"; //Guid.NewGuid() + .jpg

            var path = Path.Combine(fileProvider.GetFileInfo("files").PhysicalPath!, newFileName);

            await using var stream = new FileStream(path, FileMode.Create);

            await request.File.CopyToAsync(stream, cancellationToken);

            var response = new UploadFileCommandResponse(newFileName, $"files/{newFileName}", request.File.FileName);

            return ServiceResult<UploadFileCommandResponse>.SuccessAsCreated(response, response.FilePath);
        }
    }

    public static class UploadFileCommandEndpoint
    {
        public static RouteGroupBuilder UploadFileGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/",
                    async (IFormFile file, IMediator mediator) =>
                        (await mediator.Send(new UploadFileCommand(file))).ToGenericResult())
                .MapToApiVersion(1, 0)
                .Produces<UploadFileCommandResponse>(StatusCodes.Status201Created)
                .Produces<NotFoundType>(StatusCodes.Status400BadRequest)
                .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError).DisableAntiforgery();
            return group;
        }
    }

}
