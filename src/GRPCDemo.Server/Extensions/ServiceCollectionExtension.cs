using GRPCDemo.Server.Abstractions;
using GRPCDemo.Server.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ServiceCollectionExtension
    {
        public static IServiceCollection AddDemoService(this IServiceCollection services)
            => services.AddSingleton<DemoService>();

        public static IServiceCollection AddFileService(this IServiceCollection services)
            => services.AddSingleton<IFileService,FileService>();
    }
}
