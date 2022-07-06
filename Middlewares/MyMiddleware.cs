using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;

namespace ElzaFunctionApp.Middlewares
{
    internal class MyMiddleware : IFunctionsWorkerMiddleware
    {
        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            var logger = context.GetLogger<MyMiddleware>();
            logger.LogInformation("MyMiddleware: forward");
            await next(context);
            logger.LogInformation("MyMiddleware: backward");
        }
    }
}
