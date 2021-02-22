using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PrimeNumbers.API.Services;
using Serilog;
using PrimeNumbers.API.Interfaces;

namespace PrimeNumbers.API
{
    class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.SetMinimumLevel(LogLevel.Trace);
                    loggingBuilder.AddSerilog(new LoggerConfiguration()
                        .WriteTo.Console()
                        .WriteTo.File("app.log")
                        .CreateLogger());
                })
                .ConfigureServices(services =>
                {
                    services.AddTransient<IPrimeNumbersSevice, PrimeNumbersService>();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
