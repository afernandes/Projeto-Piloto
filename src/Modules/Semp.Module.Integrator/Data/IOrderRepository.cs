using Semp.Infrastructure.Data;
using Semp.Module.Integrator.Models;
using System;
using System.Linq;

namespace Semp.Module.Integrator.Data
{
    public interface IOrderRepository : IRepositoryQuery<OrderSendErrorView>
    {
        IQueryable<OrderSendErrorView> List();
        void ResendOrder(Guid orderId);
    }
}
