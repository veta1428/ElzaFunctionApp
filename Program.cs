using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ElzaFunctionApp.Services;
using ElzaFunctionApp.Middlewares;

namespace ElzaFunctionApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults(workerApp =>
                {
                    workerApp.UseMiddleware<MyMiddleware>();
                })
                .ConfigureServices((services) =>
                {
                    services.AddSingleton<MyService>();
                })
                .Build();

            host.Run();
        }
    }
}