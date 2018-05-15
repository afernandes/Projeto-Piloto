using Semp.Infrastructure;
using Semp.Module.DadosTransacionais.Models;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Semp.Module.DadosTransacionais.Services
{
    public interface IOrderSapService
    {
        Task<Result<IEnumerable<Order>>> GetOrderFromSap(DateTime date);
    }
}
