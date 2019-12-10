using GRPCDemo.Server.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace GRPCDemo.Server
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
            => services
                .AddDemoService()
                .AddFileService()
                .AddGrpc();
   
        public void Configure(IApplicationBuilder app)
            => app
                .UseRouting()
                .UseEndpoints(endpoints =>
                    {
                        endpoints.MapGrpcService<DemoService>();
                        endpoints.MapGet("/", async context =>
                        {
                            await context.Response.WriteAsync("Requires HTTP/2");
                        });
                    });
    }
}
