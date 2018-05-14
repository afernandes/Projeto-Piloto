using Semp.Infrastructure;
using Semp.Infrastructure.Data;
using Semp.Module.DadosTransacionais.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Semp.Module.DadosTransacionais.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;

        public OrderService(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;

        }

        public async Task<Result<Order>> CreateOrder(Order order)
        {          
            _orderRepository.Add(order);
            await _orderRepository.SaveChangesAsync();

            return Result.Ok(order);
        }

        public async Task<Result<bool>> CreateOrders(IEnumerable<Order> orders)
        {
            using (var transaction = _orderRepository.BeginTransaction())
            {
                orders.AsParallel().ForAll(order =>
                {
                    _orderRepository.Add(order);
                });
                await _orderRepository.SaveChangesAsync();
                transaction.Commit();
            }

            return Result.Ok(true);
        }

    }
}
