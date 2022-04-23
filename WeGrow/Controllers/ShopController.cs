using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using WeGrow.DAL.Interfaces;
using WeGrow.Models.Entities;

namespace WeGrow.Controllers
{
    [Route("shop")]
    public class ShopController : Controller
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public ShopController(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [Route("modules")]
        [HttpGet]
        public async Task<IActionResult> GetModules()
        {
            var items = await repository.GetRangeAsync<Module>(false, x => true);
            var itemsEntities = items.Select(x => mapper.Map<ModuleEntity>(x));
            return Ok(itemsEntities);
        }
    }
}
