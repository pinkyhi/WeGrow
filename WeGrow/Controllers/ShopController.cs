using AutoMapper;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Reflection;
using WeGrow.Core.Helpers;
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
            var filterModel = QueryMapHelper.GetModelFromQueryUrl<ModulesShopFilterModel>(this.HttpContext.Request.GetEncodedUrl());
            var filter = mapper.Map<ModulesShopFilter>(filterModel);
            var items = await repository.GetRangeAsync<DAL.Entities.Module>(false, x => true);
            var itemsEntities = items.Select(x => mapper.Map<ModuleEntity>(x));
            if (!string.IsNullOrWhiteSpace(search))
            {
                itemsEntities = itemsEntities.Where(x => x.Id.ToString().Contains(search, StringComparison.OrdinalIgnoreCase)
                                                    || x.Name.Contains(search, StringComparison.OrdinalIgnoreCase)
                                                    || x.Description.Contains(search, StringComparison.OrdinalIgnoreCase)
                                                    ).ToList();
            }
            var result = new ShopModel()
            {
                Items = itemsEntities.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                PagesCount = Convert.ToInt32(Math.Ceiling((decimal)itemsEntities.Count() / pageSize))
            };

            return Ok(result);
        }
    }
}
