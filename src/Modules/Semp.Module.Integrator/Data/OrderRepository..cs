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
        private readonly SimplDbContext _integratorContext;

        public OrderRepository(SempDbContext context, SimplDbContext integratorContext) : base(context)
        {
            _integratorContext = integratorContext;
        }
        
        public IQueryable<OrderSendErrorView> List()
        {            
            return Context.Query<OrderSendErrorView>().AsNoTracking();
        }

        public void ResendOrder(Guid orderId, string userName)
        {
            _integratorContext.Database.ExecuteSqlCommand("EXEC dbo.spc_IntegracaoReenviarPedido {0}, {1}",orderId, userName);
            //_integratorContext.SaveChanges();
        }

    }
}
