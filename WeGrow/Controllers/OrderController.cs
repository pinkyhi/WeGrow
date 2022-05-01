using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeGrow.Core.Resources;
using WeGrow.DAL.Entities;
using WeGrow.DAL.Interfaces;
using WeGrow.Models.Order;

namespace WeGrow.Controllers
{
    [Authorize]
    [Route("/orders")]
    public class OrderController : Controller
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public OrderController(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var userId = HttpContext.Request.Headers.First(x => x.Key == ConstNames.Uid).Value;
            var orders = await repository.GetRangeAsync<Order>(false, x => x.User_Id.Equals(userId), y => y.Include(i => i.Receipts).ThenInclude(i => i.Module));
            var models = orders.Select(x => mapper.Map<OrderModel>(x)).OrderBy(x => x.Status).ThenByDescending(x => x.Date);
            
            return Ok(models);
        }
    }
}
