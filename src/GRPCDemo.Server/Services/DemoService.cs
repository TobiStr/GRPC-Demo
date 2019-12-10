using Grpc.Core;
using GRPCDemo.Core;
using GRPCDemo.Server.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GRPCDemo.Server.Services
{
    public class DemoService : Core.DemoService.DemoServiceBase
    {
        private readonly ILogger logger;
        private readonly IFileService fileService;
        public DemoService(ILogger<DemoService> logger, IFileService fileService)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        }

        public override async Task<Message> Echo(Message request, ServerCallContext context)
        {
            await Task.Yield();
            logger.LogInformation($"Received message: {request.Value}");
            return new Message() { Value = request.Value };
        }

        public override async Task GetFile(FileRequest request, IServerStreamWriter<FileChunk> responseStream, ServerCallContext context)
        {
            logger.LogInformation($"Received GetFile Request: {request.ID}");
            var file = fileService.GetFile(request.ID);
            foreach (var chunk in file.ToChunks(1024))
                await responseStream.WriteAsync(chunk);
        }
    }
}
