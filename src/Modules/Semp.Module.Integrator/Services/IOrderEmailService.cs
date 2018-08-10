using System;
using System.Threading.Tasks;
using Semp.Module.Core.Models;
using Semp.Module.Integrator.Models;

namespace Semp.Module.Integrator.Services
{
    public interface IOrderEmailService
    {
        Task SendEmailToUser(User user, OrderSendErrorView order);
    }
}
