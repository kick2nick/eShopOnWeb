using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Storage.Blob;

namespace OrderItemsReserver
{
    public static class OrderItemsReserver
    {
        [FunctionName("orderitemsreserver")]
        [FixedDelayRetry(3, "00:00:10")]
        public static async Task Run(
            [ServiceBusTrigger("orders", AutoCompleteMessages = true, Connection = "ServiceBusConnection")] string orderRequest,
            [Blob("orders-container", FileAccess.Write, Connection = "AzureWebJobsOrdersStorageConnection")] CloudBlobContainer blobContainer,
            ILogger log)
        {
            var orderId = Guid.NewGuid();
            var blobName = $"{orderId}.json";

            log.LogDebug(orderRequest);

            await blobContainer.CreateIfNotExistsAsync();
            await blobContainer.GetBlockBlobReference(blobName).UploadTextAsync(orderRequest);
        } 
    }
}
