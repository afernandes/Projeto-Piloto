using Microsoft.EntityFrameworkCore;
using Semp.Module.Core.Data;
using Semp.Module.Core.Models;
using Semp.Module.Integrator.Models;
using System;
using System.Linq;

namespace Semp.Module.Integrator.Data
{
    public class OrderRepository : RepositoryQuery<OrderSendErrorView>, IOrderRepository
    {
        public OrderRepository(SempDbContext context) : base(context)
        {
        }
        
        public IQueryable<OrderSendErrorView> List()
        {
            return Context.Query<OrderSendErrorView>().AsNoTracking();
        }
    }
}
