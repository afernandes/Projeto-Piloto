using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Semp.Infrastructure.Models;
using Semp.Module.Core.Models;

namespace Semp.Module.DadosTransacionais.Models
{
    public class Order : EntityBase
    {
        public Order()
        {
            //CreatedOn = DateTimeOffset.Now;            
            //OrderStatus = OrderStatus.New;
        }

        public string PACKNO { get; set; }
        public string EXTROW { get; set; }
        public string NUMEROPEDIDO { get; set; }
        public string NUMEROPEDIDOSEMP { get; set; }
        public string DATAPEDIDO { get; set; }
        public string TIPOORDEM { get; set; }
        public string MOTIVOORDEM { get; set; }
        public string VENDEDOR { get; set; }
        public string GERENTE { get; set; }
        public string DIRETOR { get; set; }
        public string CONDICAOPAGAMENTO { get; set; }
        public string FORMAPAGAMENTO { get; set; }
        public string PEDIDOBLOQUEADO { get; set; }
        public string CODIGOCLIENTE { get; set; }
        public string CNPJCLIENTE { get; set; }
        public string CPFCLIENTE { get; set; }
        public string CONSUMIDORFINAL { get; set; }
        public string DATAENTREGA1 { get; set; }
        public string DATAENTREGA2 { get; set; }
        public string TEXTOAPROVACAO { get; set; }
        public string TEXTOCREDITO { get; set; }
        public string TEXTOLOGISTICO { get; set; }
        public string MODALIDADEFRETE { get; set; }
        public string REFERENCIANF { get; set; }
        public string REFERENCIAINVOICE { get; set; }
        public Nullable<System.DateTime> UPDATE_TIME_LEGADO { get; set; }
        public Nullable<System.DateTime> UPDATE_TIME_SAP { get; set; }        
    }
}
