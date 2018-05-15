using NSAPConnector;

using SAP.Middleware.Connector;

using Semp.Infrastructure;
using Semp.Module.DadosTransacionais.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Semp.Module.DadosTransacionais.Services
{
    public class OrderSapService : IOrderSapService
    {
        public OrderSapService()
        {
        }

        public async Task<Result<IEnumerable<Order>>> GetOrderFromSap(DateTime date)
        {
            IEnumerable<Order> orderList = null;

            try
            {
                using (var connection = new SapConnection("DEFAULT"))
                {
                    connection.Open();

                    var command = new SapCommand("ZFMSD_INTERF_DADOS_TRANSACIONA", connection);

                    string dateParam = date.ToString("yyyy-MM-dd");

                    //TESTE
                    //command.Parameters.Add("VBELN", "0060000352");
                    //dateParam = "2018-01-31";                

                    command.Parameters.Add("ERDAT1", dateParam);
                    command.Parameters.Add("ERDAT2", dateParam);
                    command.Parameters.Add("I_TP_SELECAO", "1");

                    var RFC = command.ExecuteRfc();

                    string packno = RFC.GetString("LV_PACKNO");

                    IRfcStructure ET_DADOS_TRANSACIONAIS = RFC.GetStructure("ET_DADOS_TRANSACIONAIS");

                    orderList = ObterCabOrdem(ET_DADOS_TRANSACIONAIS, date, packno);                    
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(Result.Fail<IEnumerable<Order>>(ex.Message));
            }

            return await Task.FromResult(Result.Ok(orderList));
        }


        private List<Order> ObterCabOrdem(IRfcStructure DADOS_TRANSACIONAIS, DateTime currentDate, string packno)
        {
            List<Order> cabOrdemLista = new List<Order>();

            IRfcTable CAB_ORDEM_SAP = DADOS_TRANSACIONAIS.GetTable("CAB_ORDEM");

            if (CAB_ORDEM_SAP.RowCount == 0)
                return null;

            for (int i = 0; i < CAB_ORDEM_SAP.RowCount; i++)
            {
                CAB_ORDEM_SAP.CurrentIndex = i;
                
                var order = ConvertRfcTableToOrder(CAB_ORDEM_SAP);

                order.UPDATE_TIME_SAP = currentDate;
                order.EXTROW = (i + 1).ToString();
                order.PACKNO = packno;

                cabOrdemLista.Add(order);
            }

            return cabOrdemLista;
        }

        private Order ConvertRfcTableToOrder(IRfcTable table)
        {
            var order = new Order();

            //cabOrdem.UPDATE_TIME_SAP = currentDate;
            //cabOrdem.EXTROW = (i + 1).ToString();
            //cabOrdem.PACKNO = packno;

            order.CNPJCLIENTE = table.GetString("CNPJCLIENTE");
            order.CODIGOCLIENTE = table.GetString("CODIGOCLIENTE");
            order.CONDICAOPAGAMENTO = table.GetString("CONDICAOPAGAMENTO");
            order.CONSUMIDORFINAL = table.GetString("CONSUMIDORFINAL");
            order.CPFCLIENTE = table.GetString("CPFCLIENTE");
            order.DATAENTREGA1 = table.GetString("DATAENTREGA1");
            order.DATAENTREGA2 = table.GetString("DATAENTREGA2");
            order.DATAPEDIDO = table.GetString("DATAPEDIDO");
            order.DIRETOR = table.GetString("DIRETOR");
            order.FORMAPAGAMENTO = table.GetString("FORMAPAGAMENTO");
            order.GERENTE = table.GetString("GERENTE");
            order.MODALIDADEFRETE = table.GetString("MODALIDADEFRETE");
            order.MOTIVOORDEM = table.GetString("MOTIVOORDEM");
            order.NUMEROPEDIDO = table.GetString("NUMEROPEDIDO");
            order.NUMEROPEDIDOSEMP = table.GetString("NUMEROPEDIDOSEMP");
            order.PEDIDOBLOQUEADO = table.GetString("PEDIDOBLOQUEADO");
            order.REFERENCIAINVOICE = table.GetString("REFERENCIAINVOICE");
            order.REFERENCIANF = table.GetString("REFERENCIANF");
            order.TEXTOAPROVACAO = TableToString(table.GetTable("TEXTOAPROVACAO")); //table.GetString("TEXTOAPROVACAO");
            order.TEXTOCREDITO = TableToString(table.GetTable("TEXTOCREDITO")); //table.GetString("TEXTOCREDITO");
            order.TEXTOLOGISTICO = TableToString(table.GetTable("TEXTOLOGISTICO")); //table.GetString("TEXTOLOGISTICO");
            order.TIPOORDEM = table.GetString("TIPOORDEM");
            order.VENDEDOR = table.GetString("VENDEDOR");

            return order;
        }

        private string TableToString(IRfcTable rfcTable)
        {
            string ret = string.Empty;

            foreach (IRfcStructure row in rfcTable)
            {
                ret += row.GetString(0) + "\r\n";
            }

            return ret.TrimEnd();
        }

    }
}
