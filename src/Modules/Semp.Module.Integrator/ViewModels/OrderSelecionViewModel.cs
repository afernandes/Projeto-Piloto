using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Semp.Module.Integrator.ViewModels
{
    [Serializable]
    public class OrderSelecionViewModel
    {
        public List<OrderDetailVm> Order { get; set; }
        public OrderSelecionViewModel()
        {
            this.Order = new List<OrderDetailVm>();
        }


        public IEnumerable<Guid?> getSelectedIds()
        {
            // Return an Enumerable containing the Id's of the selected people:
            return (from p in this.Order where p.Selected select p.Id).ToList();
        }

    }
}
