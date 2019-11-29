using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace AzFunction.LoL
{
    public static class LoL
    {
        [FunctionName(nameof(GetLoLz))]
        public static IActionResult GetLoLz(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")]
            HttpRequest request,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            return int.TryParse(request.Query["count"], out var count)
                ? (ActionResult)new OkObjectResult($"{string.Join(string.Empty, Enumerable.Repeat("LoL.", count))}")
                : new BadRequestObjectResult("Please pass a count on the query string");
        }
    }
}