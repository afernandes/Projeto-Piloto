using System;
using System.Threading.Tasks;
using Semp.Infrastructure;
using Semp.Module.Core.Models;
using Semp.Module.Integrator.Models;

namespace Semp.Module.Integrator.Services
{
    public interface IOrderService
    {        
        void ResendOrder(OrderSendErrorView order);        
    }
}
