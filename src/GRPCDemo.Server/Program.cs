using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GRPCDemo.Server
{
    public static class Program
    {
        public static void Main(string[] args)
            => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) 
            => Host.CreateDefaultBuilder(args)
                .ConfigureLogging(builder => builder.AddConsole())
                .ConfigureWebHostDefaults(webBuilder
                    => webBuilder.UseStartup<Startup>()
                );
    }
}
