using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ElzaFunctionApp.Services;
using ElzaFunctionApp.Middlewares;
using Azure.Storage.Blobs;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Security.KeyVault.Secrets;
using Azure.Identity;
using Microsoft.Extensions.Configuration;

namespace ElzaFunctionApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureAppConfiguration((context, config) =>
                {
                    var configuration = config.Build();
                    var configurationOptions = new AzureKeyVaultConfigurationOptions();
                    var secretClient = new SecretClient(
                        new Uri(Environment.GetEnvironmentVariable("KeyVault")!),
                        new DefaultAzureCredential());
                    config.AddAzureKeyVault(secretClient, configurationOptions);

                })
                .ConfigureFunctionsWorkerDefaults(workerApp =>
                {
                    workerApp.UseMiddleware<MyMiddleware>();
                })
                .ConfigureServices((context, services) =>
                {
                    var c = context.Configuration;
                    services.AddSingleton<MyService>();
                    services.AddSingleton(new BlobServiceClient(context.Configuration["AzureBlobStorage"]).GetBlobContainerClient("test-test"));
                    services.AddSingleton<IBlobService, BlobService>();
                })
                .Build();

            host.Run();
        }
    }
}