using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using ClickStream.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace LoadGenFuncApp
{
    public static class Click
    {
        [FunctionName("click")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]HttpRequest req, ExecutionContext context, TraceWriter log)
        {
            var list = new[]
              {
                    "Cosmos DB", "Table Storage", "SQL Database", "Data Lake", "SQL Data Warehouse", "Azure Search",
                    "MySQL", "PostGreSQL", "Redis Cache"
                };

            var r = new Random();
            var index = r.Next() % (list.Length - 1);

            var selection = list[index];

            var csEvent = new ClickEvent
            {
                Id = context.InvocationId.ToString(),
                CampaignId = 20181004,
                Source = "load-gen",
                Value = selection
            };

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(Environment.GetEnvironmentVariable("CLICK_STREAM_API_URI"),
                                       new ObjectContent(typeof(ClickEvent),
                                       csEvent,
                                       new JsonMediaTypeFormatter()));

                if (response.IsSuccessStatusCode)
                {
                    return new OkObjectResult($"OK:{selection}");
                }

                return new BadRequestObjectResult($"oups? {response.ReasonPhrase}");
            }
        }
    }
}
