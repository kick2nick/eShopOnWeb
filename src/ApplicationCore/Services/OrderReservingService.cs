using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.Extensions.Configuration;
using Azure.Messaging.ServiceBus;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using System.Text.Json;

namespace Microsoft.eShopWeb.ApplicationCore.Services
{
    public class OrderReservingService : IOrderReservingService
    {
        private readonly ServiceBusClient _client;
        private readonly ServiceBusSender _sender;
        public OrderReservingService(IConfiguration config)
        {
            _client = new ServiceBusClient(config.GetConnectionString("ServiceBusConnection"));
            _sender = _client.CreateSender(config["ServiceBus:OrdersQueue"]);
        }

        public async ValueTask DisposeAsync()
        {
            await _client.DisposeAsync();
            await _sender.DisposeAsync();
        }

        public async Task ReserveAsync(Order order)
        {
            var message = new ServiceBusMessage(JsonSerializer.Serialize(order));
            await _sender.SendMessageAsync(message);
        }
    }
}
