using Grpc.Net.Client;
using System;

namespace GRPCDemo.Client
{
    public class DemoClientFactory : IDisposable
    {
        private readonly DemoClient demoClient;
        private readonly GrpcChannel channel;

        public DemoClientFactory(string remotehost)
        {
            if (string.IsNullOrWhiteSpace(remotehost)) throw new ArgumentException(nameof(remotehost));
            channel = GrpcChannel.ForAddress(remotehost);
            demoClient = new DemoClient(new Core.DemoService.DemoServiceClient(channel));
        }

        public IDemoClient GetService()
            => demoClient;

        public void Dispose() 
            => channel.Dispose();
    }
}
