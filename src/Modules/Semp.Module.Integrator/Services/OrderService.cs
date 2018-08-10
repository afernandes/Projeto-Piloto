using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Semp.Infrastructure;
using Semp.Infrastructure.Data;
using Semp.Module.Core.Models;
using Semp.Module.Integrator.Models;
using Semp.Module.Integrator.ViewModels;

namespace Semp.Module.Integrator.Services
{
    public class OrderService : IOrderService
    {        
        private readonly IRepositoryQuery<OrderSendErrorView> _orderRepository;                
        private readonly IOrderEmailService _orderEmailService;

        public OrderService(IRepositoryQuery<OrderSendErrorView> orderRepository,            
            IOrderEmailService orderEmailService)
        {
            _orderRepository = orderRepository;           
            _orderEmailService = orderEmailService;
        }

        public void ResendOrder(OrderSendErrorView order)
        {                       
        }

    }
}
