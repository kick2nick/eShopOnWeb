using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Services
{
    public class OrderDeliveryService : IOrderDeliveryService
    {
        private readonly IConfiguration _configuration;

        public OrderDeliveryService(IConfiguration  config)
        {
            _configuration = config;
        }

        public Task Submit(Order order) =>
             new HttpClient()
                .PostAsync(_configuration["baseUrls:DeliveryFuncUrl"], new StringContent(JsonSerializer.Serialize(order)));
    }
}