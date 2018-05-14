using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using Semp.Infrastructure.ScheduledTasks;
using System.Linq;
using Semp.Infrastructure.Data;
using Semp.Module.DadosTransacionais.Models;
using Semp.Infrastructure;

namespace Semp.Module.DadosTransacionais.Services
{
    public class OrderSapToLegacyBackgroundService : IScheduledTask
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _config;
        private readonly IOrderService _orderService;
        private readonly IOrderServiceSap _orderServiceSap;
        private readonly IRepository<Order> _orderRepository;

        public OrderSapToLegacyBackgroundService(IMediator mediator, IConfiguration config,
            IOrderService orderService, IOrderServiceSap orderServiceSap,
            IRepository<Order> orderRepository)
        {
            _mediator = mediator;
            _config = config;
            _orderService = orderService;
            _orderServiceSap = orderServiceSap;
            _orderRepository = orderRepository;
        }

        public string Schedule => "* */10 * * * *";

        public async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (stoppingToken.IsCancellationRequested) return;
            await Testando(stoppingToken);
        }

        private async Task Testando(CancellationToken stoppingToken)
        {
            Result<IEnumerable<Order>> resultFromSap = await _orderServiceSap.GetOrderFromSap(DateTime.Now);

            if (!resultFromSap.Success)
                return;

            var ordersFromSap = resultFromSap.Value;

            //using (var transaction = _orderRepository.BeginTransaction())
            //{
                ordersFromSap.AsParallel().ForAll(order =>
                {
                    _orderRepository.Add(order);
                });
                await _orderRepository.SaveChangesAsync();
               //transaction.Commit();
            //}

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
