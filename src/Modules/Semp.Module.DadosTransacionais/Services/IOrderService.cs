﻿using Semp.Infrastructure;
using Semp.Module.DadosTransacionais.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Semp.Module.DadosTransacionais.Services
{
    public interface IOrderService
    {
        Task<Result<Order>> CreateOrder(Order order);
        Task<Result<bool>> CreateOrders(IEnumerable<Order> orders);
    }
}
