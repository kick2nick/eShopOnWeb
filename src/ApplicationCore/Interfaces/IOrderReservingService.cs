using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using System;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces
{
    public interface IOrderReservingService : IAsyncDisposable
    {
        public Task ReserveAsync(Order order);
    }
}
