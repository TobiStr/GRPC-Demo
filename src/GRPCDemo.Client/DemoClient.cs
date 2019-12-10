using GRPCDemo.Core;
using System;
using System.Threading.Tasks;

namespace GRPCDemo.Client
{
    internal class DemoClient : IDemoClient
    {
        private readonly Core.DemoService.DemoServiceClient client;

        public DemoClient(DemoService.DemoServiceClient client)
            => this.client = client ?? throw new ArgumentNullException(nameof(client));

        public async Task<string> Echo(string Message)
            => (await client.EchoAsync(new Message() { Value = Message })).Value;

        public async Task<File> GetFile(string id)
            => await File.FromChunkStream(client.GetFile(new FileRequest() { ID = id }).ResponseStream);
    }
}
