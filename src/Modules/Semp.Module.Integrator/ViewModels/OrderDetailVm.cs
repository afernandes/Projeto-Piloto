using System;
using System.Collections.Generic;

namespace Semp.Module.Integrator.ViewModels
{
    [Serializable]
    public class OrderDetailVm
    {
        public OrderDetailVm(){ }

        public Guid? Id { get; set; }
        public string OrderType{ get; set; }
        public string OrderLegacy{ get; set; }
        public string Attendence { get; set; }
        public DateTime? UpdateTimeSap{ get; set; }
        public string Error { get; set; }
        public string DocumentNumber { get; set; }
        public string ClientName{ get; set; }

        public bool Selected { get; set; }

    }
}
