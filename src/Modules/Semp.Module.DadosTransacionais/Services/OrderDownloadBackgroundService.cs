using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Semp.Infrastructure;
using Semp.Infrastructure.Data;
//using Semp.Module.DadosTransacionais.Events;
using Semp.Module.DadosTransacionais.Models;


namespace Semp.Module.DadosTransacionais.Services
{
    public class OrderDownloadBackgroundService : BackgroundService
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Order> _orderRepository;
        private readonly IOrderService _orderService;
        private readonly long SystemUserId = 2;

        public OrderDownloadBackgroundService(IMediator mediator, IRepository<Order> orderRepository, IOrderService orderService)
        {
            _mediator = mediator;
            _orderRepository = orderRepository;
            _orderService = orderService;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await CancelFailedPaymentOrders(stoppingToken);
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }

        private async Task CancelFailedPaymentOrders(CancellationToken stoppingToken)
        {
            System.Diagnostics.Debug.Print("teste background " + DateTime.Now.ToString());

            /*
            var shouldCancelFailedPaymentOrders = _orderRepository.Query().Where(x =>
            (x.OrderStatus == OrderStatus.PendingPayment || x.OrderStatus == OrderStatus.PaymentFailed)
            && x.UpdatedOn < DateTimeOffset.Now.AddMinutes(-5));
            foreach (var order in shouldCancelFailedPaymentOrders)
            {
                _orderService.CancelOrder(order);
                var orderStatusChanged = new OrderChanged
                {
                    OrderId = order.Id,
                    OldStatus = OrderStatus.PendingPayment,
                    NewStatus = OrderStatus.Canceled,
                    UserId = SystemUserId,
                    Order = order,
                    Note = "System canceled"
                };

                await _mediator.Publish(orderStatusChanged, stoppingToken);
            }

            await _orderRepository.SaveChangesAsync();
            */
        }
    }

}
