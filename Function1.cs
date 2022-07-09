using System.Collections.Generic;
using System.Net;
using ElzaFunctionApp.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ElzaFunctionApp
{
    internal class Function1
    {
        private readonly ILogger _logger;
        private readonly MyService _myService;
        private readonly IBlobService _blobService;
        private readonly IConfiguration _configuration;

        public Function1(ILoggerFactory loggerFactory, MyService myService, IBlobService blobService, IConfiguration configuration)
        {
            _logger = loggerFactory.CreateLogger<Function1>();
            _myService = myService;
            _blobService = blobService;
            _configuration = configuration;
        }

        [Function("Function1")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "summer/{name:alpha}")] HttpRequestData req, 
            string name)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            _logger.LogInformation("MyService says: " + _myService.SayHello());

            var str = await _blobService.GetDataAsync("content.html");
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/html; charset=utf-8");
            response.WriteString(String.Format(str, name));
            var value = Environment.GetEnvironmentVariable("MyValue");
            Console.WriteLine($"Retrieved config value: {value}");
            return response;
        }
    }
}
