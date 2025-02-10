using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using UdemyMicroservices.Shared;
using UdemyMicroservices.Shared.Extensions;
using UdemyMicroservices.Shared.ProduceTypes;

namespace UdemyMicroservice.File.Api.Features.Delete
{
    public sealed record DeleteFileCommand(string FileNameWithExtension) : IRequestByServiceResult;

    public sealed class DeleteFileCommandHandler(IFileProvider fileProvider) : IRequestHandler<DeleteFileCommand, ServiceResult>
    {
        public Task<ServiceResult> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var fileInfo = fileProvider.GetFileInfo(Path.Combine("files", request.FileNameWithExtension));
            if (!fileInfo.Exists)
            {
                return Task.FromResult(ServiceResult.ErrorAsNotFound());
            }
            System.IO.File.Delete(fileInfo.PhysicalPath!);
            return Task.FromResult(ServiceResult.SuccessAsNoContent());
        }
    }
    public static class DeleteFileCommandEndpoint
    {
        public static RouteGroupBuilder DeleteFileGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/",
                    async([FromBody] DeleteFileCommand command, [FromServices] IMediator mediator) =>
                        (await mediator.Send(command)).ToGenericResult())
                .MapToApiVersion(1, 0)
                .Produces(StatusCodes.Status204NoContent)
                .Produces<NotFoundType>(StatusCodes.Status404NotFound)
                .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
            return group;
        }
    }
}
