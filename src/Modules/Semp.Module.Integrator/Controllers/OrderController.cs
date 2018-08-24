using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Semp.Infrastructure.Data;
using Semp.Module.Core.Extensions;
using Semp.Module.Core.Models;
using Semp.Module.Core.Services;
using Semp.Module.Integrator.Data;
using Semp.Module.Integrator.ViewModels;

namespace Semp.Module.Integrator.Controllers
{
    [Authorize(Roles = "dat,internet,dat troca")]
    [Route("integrator/order")]
    public class OrderController : Controller
    {
        private readonly IMediaService _mediaService;
        private readonly IOrderRepository _orderRepository;
        private readonly IWorkContext _workContext;
        private readonly IRepository<User> _userRepository;
        //private readonly IOrderService _orderService;

        public OrderController(
            IOrderRepository orderRepository,
            IWorkContext workContext,
            IMediaService mediaService
            //IRepository<User> userRepository
            /*,IOrderService orderService*/)
        {
            _orderRepository = orderRepository;
            _workContext = workContext;
            _mediaService = mediaService;
            //_userRepository = userRepository;
            //_orderService = orderService;
        }


        [HttpGet("send-errors")]
        public async Task<IActionResult> OrderErrorList()
        {
            //var user = await _workContext.GetCurrentUser();
            /*var rolesNames = _userRepository.Query()
                .Where(x => x.UserGuid == user.UserGuid)
                .Include(x => x.Roles)
                    .ThenInclude(x => x.Role)
                .Select(x => x.Roles.SelectMany(y => y.Role.Name)).ToList();*/

            var rolesNames = _workContext.GetRolesForCurrentUser();

            var orders = (await _orderRepository.List().ToListAsync())
                .Where(x => rolesNames.Contains(x.ORDER_TYPE, StringComparer.InvariantCultureIgnoreCase))
                .Select(x => new OrderDetailVm
                {
                    Id = x.Id,
                    OrderType = x.ORDER_TYPE,
                    OrderLegacy = x.PEDIDO_LEGADO,
                    Attendence = x.LOTE_ATENDIMENTO,
                    UpdateTimeSap = x.UPDATE_TIME_SAP,
                    Error = x.ERRO,
                    DocumentNumber = x.CNPJ,
                    ClientName = x.Cliente,
                    Selected = false
                })
                .OrderByDescending(x => x.UpdateTimeSap);

            var model = new OrderSelecionViewModel();
            model.Order.AddRange(orders);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitSelected(OrderSelecionViewModel model)
        {
            // get the ids of the items selected:
            var selectedIds = model.getSelectedIds();

            // Use the ids to retrieve the records for the selected people
            // from the database:
            var selectedOrder = await _orderRepository.List().Where(x => selectedIds.Contains(x.Id)).ToListAsync();

            var user = await _workContext.GetCurrentUser();
            var userName = user.UserName;

            // Process according to your requirements:
            foreach (var orderId in selectedOrder.Where(x => x.Id.HasValue).Select(s => s.Id.Value))
            {
                _orderRepository.ResendOrder(orderId, userName);
            }

            // Redirect somewhere meaningful (probably to somewhere showing 
            // the results of your processing):
            return RedirectToAction("OrderErrorList");
        }

        [HttpPost("resend")]
        public async Task<IActionResult> Resend(Guid id)
        {
            var user = await _workContext.GetCurrentUser();

            var userName = user.UserName;

            _orderRepository.ResendOrder(id, userName);

            return RedirectToAction("OrderErrorList");
        }

    }
}