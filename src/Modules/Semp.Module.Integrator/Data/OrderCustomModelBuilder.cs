using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Semp.Infrastructure.Data;
using Semp.Module.Integrator.Models;

namespace Semp.Module.Integrator.Data
{
    public class OrderCustomModelBuilder : ICustomModelBuilder
    {
        public void Build(ModelBuilder modelBuilder)
        {            
            modelBuilder.Query<OrderSendErrorView>().ToView("viw_PedidoSapErroComCliente");
        }
    }
}
