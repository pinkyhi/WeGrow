using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using WeGrow.DAL.Interfaces;
using WeGrow.Models.Entities;
using WeGrow.Models.Shop;

namespace WeGrow.Controllers
{
    [Route("shop")]
    public class ShopController : Controller
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;
        private readonly int pageSize = 12;
        public ShopController(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [Route("modules")]
        [HttpGet]
        public async Task<IActionResult> GetModules([FromQuery] int page, [FromQuery] string search)
        {
            var items = await repository.GetRangeAsync<DAL.Entities.Module>(false, x => true);
            var itemsEntities = items.Select(x => mapper.Map<ModuleEntity>(x));

            var result = new ShopModel()
            {
                Items = itemsEntities.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                PagesCount = Convert.ToInt32(Math.Ceiling((decimal)itemsEntities.Count() / pageSize))
            };

            return Ok(result);
        }
    }
}
