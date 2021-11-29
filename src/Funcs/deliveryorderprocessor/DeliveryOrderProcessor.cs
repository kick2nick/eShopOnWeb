using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace deliveryorderprocessor
{
    public static class DeliveryOrderProcessor
    {
        [FunctionName("DeliveryOrderProcessor")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log,
            [CosmosDB("OrdersDb", "ordersCollection" ,CreateIfNotExists = true,
            ConnectionStringSetting = "CosmosDBConnection")] IAsyncCollector<dynamic> documentsOut)
        {
            using var sr = new StreamReader(req.Body);
            var order = await sr.ReadToEndAsync();

            log.LogInformation(order);

            if (!string.IsNullOrEmpty(order))
            {
                await documentsOut.AddAsync(order);
                return new OkResult();
            }
            return new BadRequestResult();
        }
    }
}