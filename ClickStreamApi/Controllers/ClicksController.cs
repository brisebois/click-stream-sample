using System;
using System.Text;
using System.Threading.Tasks;
using ClickStream.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;

namespace ClickStreamApi.Controllers
{
    [Route("api/[controller]")]
    public class ClicksController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return "ok";
        }

        [HttpPost]
        public async Task Post([FromBody]ClickEvent csevent)
        {
            var client = EventHubClient.CreateFromConnectionString(Environment.GetEnvironmentVariable("EVENTHUBS_CS"));
            
            var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(csevent));
            var ed = new EventData(bytes);
            await client.SendAsync(ed);
        }
    }
}
