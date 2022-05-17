using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Extensions.DependencyInjection;
using Savvyio;

namespace Savvyio_Example
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hb = CreateHostBuilder(args).Build();
            var descriptor = hb.Services.GetRequiredService<HandlerServicesDescriptor>();
            Console.WriteLine(descriptor);
            hb.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
