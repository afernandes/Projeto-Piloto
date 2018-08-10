using System;

namespace Semp.Module.Integrator.Models
{
    public class OrderSendErrorView //: IEntityWithTypedId<Guid> //: EntityBase
    {
        public OrderSendErrorView()
        {
        }

        public Guid Id { get; set; }

        //public Guid ORDER_HEADER_ID { get; set; }
        public string ORDER_TYPE { get; set; }
        public string PEDIDO_LEGADO { get; set; }
        public string LOTE_ATENDIMENTO { get; set; }
        public DateTime UPDATE_TIME_SAP { get; set; }
        public string ERRO { get; set; }
        public string CNPJ { get; set; }
        public string Cliente { get; set; }
       
    }
}
