using System;
using Semp.Module.Core.Models;

namespace Semp.Module.Integrator.Models
{
    public class PedidoLiberado : Infrastructure.Models.Entity
    {
        public PedidoLiberado()
        {
            LiberadoEm = DateTime.Now;
        }

        public string Pedido { get; set; }
        public string Tipo { get; set; }
        public string Regra { get; set; }
        public User LiberadoPor { get; set; }
        public DateTime LiberadoEm { get; set; }
    }
}
