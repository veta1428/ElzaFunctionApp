using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ElzaFunctionApp.Services;
using ElzaFunctionApp.Middlewares;
using Azure.Storage.Blobs;

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
                    services.AddSingleton(new BlobServiceClient(Environment.GetEnvironmentVariable("AzureBlobStorage")).GetBlobContainerClient("test-test"));
                    services.AddSingleton<IBlobService, BlobService>();
                })
                .Build();

            host.Run();
        }
    }
}