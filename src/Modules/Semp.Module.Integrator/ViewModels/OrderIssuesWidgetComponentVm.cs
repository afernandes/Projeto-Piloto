using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Semp.Module.Integrator.ViewModels
{
    public class OrderIssuesWidgetComponentVm
    {
        public long Id { get; set; }

        public string WidgetName { get; set; }

        public List<OrderIssuesWidgetPanelVm> Items { get; set; }
    }
}
