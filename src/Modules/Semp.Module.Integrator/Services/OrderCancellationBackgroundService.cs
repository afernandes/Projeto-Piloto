using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Semp.Infrastructure;
using Semp.Infrastructure.Data;
//using Semp.Module.Integrator.Events;
using Semp.Module.Integrator.Models;
using Semp.Module.Integrator.Data;

namespace Semp.Module.Integrator.Services
{
    public class OrderCancellationBackgroundService : BackgroundService
    {
        private readonly IMediator _mediator;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderService _orderService;
        private readonly long SystemUserId = 2;

        public OrderCancellationBackgroundService(IMediator mediator, IOrderRepository orderRepository, IOrderService orderService)
        {
            _mediator = mediator;
            _orderRepository = orderRepository;
            _orderService = orderService;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await SendMailFailedSendOrders(stoppingToken);
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }

        private async Task SendMailFailedSendOrders(CancellationToken stoppingToken)
        {
            var shouldCancelFailedPaymentOrders = _orderRepository.List();
            foreach(var order in shouldCancelFailedPaymentOrders)
            {
                //_orderService.ResendOrder(order.ORDER_HEADER_ID);
                
                /*var orderStatusChanged = new OrderChanged
                {
                    OrderId = order.Id,
                    OldStatus = OrderStatus.PendingPayment,
                    NewStatus = OrderStatus.Canceled,
                    UserId = SystemUserId,
                    Order = order,
                    Note = "System canceled"
                };*/

                //await _mediator.Publish(orderStatusChanged, stoppingToken);
            }

            //await _orderRepository.SaveChangesAsync();
        }
    }
}
