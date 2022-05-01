using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeGrow.Core.Resources;
using WeGrow.DAL.Entities;
using WeGrow.DAL.Interfaces;
using WeGrow.LiqPay.Interfaces;
using WeGrow.LiqPay.Models;
using WeGrow.LiqPay.Static;
using WeGrow.Models.Order;

namespace WeGrow.Controllers
{
    [Authorize]
    [Route("/orders")]
    public class OrderController : Controller
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;
        private readonly ILiqPayService liqPayService;

        public OrderController(IRepository repository, IMapper mapper, ILiqPayService liqPayService)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.liqPayService = liqPayService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var userId = HttpContext.Request.Headers.First(x => x.Key == ConstNames.Uid).Value;
            var orders = await repository.GetRangeAsync<Order>(false, x => x.User_Id.Equals(userId), y => y.Include(i => i.Receipts).ThenInclude(i => i.Module));
            var models = orders.Select(x => mapper.Map<OrderModel>(x)).OrderBy(x => x.Status).ThenByDescending(x => x.Date);
            
            return Ok(models);
        }

        [HttpPatch]
        public async Task<IActionResult> DeleteOrder([FromBody] int id)
        {
            var userId = HttpContext.Request.Headers.First(x => x.Key == ConstNames.Uid).Value;

            var order = await repository.GetAsync<Order>(false, x => x.Id == id && x.User_Id.Equals(userId));
            if(order != null)
            {
                await repository.DeleteAsync(order);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("liqpay-checkout")]
        [HttpPost]
        public async Task<IActionResult> LiqPayCheckout([FromBody] int orderId)
        {
            var order = await repository.GetAsync<Order>(false, x => x.Id == orderId, y => y.Include(i => i.Receipts));
            if (order != null)
            {
                var checkoutModel = new LiqPayCheckoutModel()
                {
                    Order_Id = order.Id.ToString(),
                    Sandbox = 1,
                    Action = LiqPayStatic.Actions.Pay,
                    Description = $"Payment for order #{order.Id}\nWeGrow company.",
                    Amount = order.Receipts.Sum(x => x.Amount * x.Cache_Price),
                    Currency = "UAH"
                };
                var (data, signature) = this.liqPayService.EncryptLiqPay(checkoutModel);

                return Ok(new DataSignaturePair(data, signature));
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("liqpay-notification")]
        [HttpPost]
        public IActionResult Notifications(DataSignaturePair request)
        {
            if (this.liqPayService.CheckDataBySignature(request.Data, request.Signature))
            {
                LiqPayAnswerModel model = this.liqPayService.AnswerModelFromData(request.Data);
                // Model rework
            }
            else
            {
                return BadRequest();
            }

            return this.Ok();
        }
    }
}
