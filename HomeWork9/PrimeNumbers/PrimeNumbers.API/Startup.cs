using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PrimeNumbers.API.Interfaces;
using PrimeNumbers.API.Models;

namespace PrimeNumbers.API
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    var logger = context.RequestServices.GetRequiredService<ILogger<Startup>>();

                    var loggingInfo = $"Request: {context.Request.Path}\n";

                    await context.Response.WriteAsync("Prime Numbers API by Daniil Panasenko");

                    loggingInfo += $"Status code (expected): {(int)HttpStatusCode.OK}\n";
                    loggingInfo += $"Status code (actual): {context.Response.StatusCode}\n";

                    logger.LogInformation(loggingInfo);
                });

                endpoints.MapGet("/primes/{number:int}", async context =>
                {
                    var service = context.RequestServices.GetRequiredService<IPrimeNumbersSevice>();
                    var logger = context.RequestServices.GetRequiredService<ILogger<Startup>>();

                    var loggingInfo = $"Request: {context.Request.Path}\n";

                    var number = int.Parse((string)context.Request.RouteValues["number"]);

                    var isPrime = await service.IsPrimeAsync(number);

                    loggingInfo += isPrime
                        ? $"Status code (expected): { (int)HttpStatusCode.OK}\n"
                        : $"Status code (expected): { (int)HttpStatusCode.NotFound}\n";

                    context.Response.StatusCode = isPrime
                        ? (int)HttpStatusCode.OK
                        : (int)HttpStatusCode.NotFound;

                    loggingInfo += $"Status code (actual): {context.Response.StatusCode}";

                    logger.LogInformation(loggingInfo);
                });

                endpoints.MapGet("/primes", async context =>
                {
                    var service = context.RequestServices.GetRequiredService<IPrimeNumbersSevice>();
                    var logger = context.RequestServices.GetRequiredService<ILogger<Startup>>();

                    var loggingInfo = $"Request: {context.Request.Path}\n";

                    var inputFrom = context.Request.Query["from"].FirstOrDefault();
                    var inputTo = context.Request.Query["to"].FirstOrDefault();

                    int from, to;
                    try
                    {
                        from = int.Parse(inputFrom);
                        to = int.Parse(inputTo);
                        loggingInfo += $"Status code (expected): { (int)HttpStatusCode.OK}\n";
                    }
                    catch (Exception)
                    {
                        loggingInfo += $"Status code (expected): { (int)HttpStatusCode.BadRequest}\n";
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        await context.Response.WriteAsync("Invalid parametres");
                        loggingInfo += $"Status code (actual): {context.Response.StatusCode}\n";
                        return;
                    }

                    var settings = new PrimesSettings(from, to);
                    var primes = await service.GetPrimesAsync(settings);

                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    await context.Response.WriteAsync($"{string.Join(',', primes)}");

                    loggingInfo += $"Status code (actual): {context.Response.StatusCode}\n";

                    logger.LogInformation(loggingInfo);
                });
            });
        }
    }
}