using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Semp.Infrastructure;
using Semp.Infrastructure.Data;
using Semp.Infrastructure.Web.SmartTable;
using Semp.Module.Integrator.Models;
using Semp.Module.Integrator.ViewModels;
using Semp.Module.Core.Extensions;
//using Semp.Module.Integrator.Events;

namespace Semp.Module.Integrator.Controllers
{
    [Authorize(Roles = "admin, vendor")]
    [Route("api/integrator")]
    public class OrderApiController : Controller
    {
        private readonly IRepositoryQuery<OrderSendErrorView> _orderRepository;
        private readonly IWorkContext _workContext;
        private readonly IMediator _mediator;

        public OrderApiController(IRepositoryQuery<OrderSendErrorView> orderRepository, IWorkContext workContext, IMediator mediator)
        {
            _orderRepository = orderRepository;
            _workContext = workContext;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> Get(int status, int numRecords)
        {
            if ((numRecords <= 0) || (numRecords > 100))
            {
                numRecords = 5;
            }

            var query = _orderRepository.Query();


            var currentUser = await _workContext.GetCurrentUser();
            if (!User.IsInRole("admin"))
            {
                query = query.Where(x => x.ORDER_TYPE == currentUser.VendorId.ToString());
            }

            var model = query.OrderByDescending(x => x.UPDATE_TIME_SAP)
                .Take(numRecords);

            return Json(model);
        }

        [HttpPost("grid")]
        public async Task<ActionResult> List([FromBody] SmartTableParam param)
        {
            IQueryable<OrderSendErrorView> query = _orderRepository
                .Query();

            var currentUser = await _workContext.GetCurrentUser();
            if (!User.IsInRole("admin"))
            {
                query = query.Where(x => x.ORDER_TYPE == currentUser.VendorId.ToString());
            }

            if (param.Search.PredicateObject != null)
            {
                dynamic search = param.Search.PredicateObject;
                if (search.ORDER_HEADER_ID != null)
                {
                    Guid ORDER_HEADER_ID = search.ORDER_HEADER_ID;
                    query = query.Where(x => x.Id == ORDER_HEADER_ID);
                }


                if (search.Cliente != null)
                {
                    string cliente = search.Cliente;
                    query = query.Where(x => x.Cliente.Contains(cliente));
                }

                if (search.UPDATE_TIME_SAP != null)
                {
                    if (search.UPDATE_TIME_SAP.before != null)
                    {
                        DateTimeOffset before = search.UPDATE_TIME_SAP.before;
                        query = query.Where(x => x.UPDATE_TIME_SAP <= before);
                    }

                    if (search.UPDATE_TIME_SAP.after != null)
                    {
                        DateTimeOffset after = search.UPDATE_TIME_SAP.after;
                        query = query.Where(x => x.UPDATE_TIME_SAP >= after);
                    }
                }
            }

            /*
            var orders = query.ToSmartTableResult(
                param,
                order => new
                {
                    order.Id,
                    order.ORDER_TYPE,
                    order.PEDIDO_LEGADO,
                    order.LOTE_ATENDIMENTO,
                    order.UPDATE_TIME_SAP,
                    order.ERRO,
                    order.CNPJ,
                    order.Cliente
                });
                */

            var orders = await query.Select(order => new
            {
                id = order.Id,
                order_type = order.ORDER_TYPE,
                pedido_legado = order.PEDIDO_LEGADO,
                lote_atendimento = order.LOTE_ATENDIMENTO,
                update_time_sap = order.UPDATE_TIME_SAP,
                erro = order.ERRO,
                cnpj = order.CNPJ,
                cliente = order.Cliente
            }).ToListAsync();

            return Json(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var order = _orderRepository
                .Query()
                .FirstOrDefault(x => x.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            var currentUser = await _workContext.GetCurrentUser();
            if (!User.IsInRole("admin") && order.ORDER_TYPE != currentUser.VendorId.ToString())
            {
                return BadRequest(new { error = "You don't have permission to manage this order" });
            }

            var model = new 
            {
                id = order.Id,
                order_type = order.ORDER_TYPE,
                pedido_legado = order.PEDIDO_LEGADO,
                lote_atendimento = order.LOTE_ATENDIMENTO,
                update_time_sap = order.UPDATE_TIME_SAP,
                erro = order.ERRO,
                cnpj = order.CNPJ,
                cliente = order.Cliente
            };

            //await _mediator.Publish(new OrderDetailGot { OrderDetailVm = model });

            return Json(model);
        }

        [HttpPost("change-order-status/{id}")]
        public async Task<IActionResult> ResendStatus(Guid id)
        {
            var order = _orderRepository.Query().FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            var currentUser = await _workContext.GetCurrentUser();
            if (!User.IsInRole("admin") && order.ORDER_TYPE != currentUser.VendorId.ToString())
            {
                return BadRequest(new { error = "You don't have permission to manage this order" });
            }

            /*if (Enum.IsDefined(typeof(OrderStatus), model.StatusId))
            {
                var oldStatus = order.OrderStatus;
                order.OrderStatus = (OrderStatus)model.StatusId;
                await _orderRepository.SaveChangesAsync();

                var orderStatusChanged = new OrderChanged
                {
                    OrderId = order.Id,
                    OldStatus = oldStatus,
                    NewStatus = order.OrderStatus,
                    Order = order,
                    UserId = currentUser.Id,
                    Note = model.Note
                };

                await _mediator.Publish(orderStatusChanged);
                return Accepted();
            }*/
            return Accepted(); //teste

            return BadRequest(new { Error = "unsupported order status" });
        }

        /*[HttpGet("order-status")]
        public IActionResult GetOrderStatus()
        {
            var model = EnumHelper.ToDictionary(typeof(OrderStatus)).Select(x => new { Id = x.Key, Name = x.Value });
            return Json(model);
        }*/
    }
}