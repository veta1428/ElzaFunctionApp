using System.Collections.Generic;
using System.Net;
using ElzaFunctionApp.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace ElzaFunctionApp
{
    internal class Function1
    {
        private readonly ILogger _logger;
        private readonly MyService _myService;

        public Function1(ILoggerFactory loggerFactory, MyService myService)
        {
            _logger = loggerFactory.CreateLogger<Function1>();
            _myService = myService;
        }

        [Function("Function1")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "summer/{name:alpha}")] HttpRequestData req, string name)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            _logger.LogInformation("MyService says: " + _myService.SayHello());

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/html; charset=utf-8");
            string contents = File.ReadAllText("content.html");
            response.WriteString(String.Format(contents, name));
            var value = Environment.GetEnvironmentVariable("MyValue");
            Console.WriteLine($"Retrieved config value: {value}");
            return response;
        }
    }
}
