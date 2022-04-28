using AutoMapper;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Reflection;
using WeGrow.Core.Enums;
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

            // Filtering
            var items = await repository.GetRangeAsync<DAL.Entities.Module>(false, x => 
            {
                bool result = true;

                if(result && filterModel.IsInStock != null)
                {
                    result = x.Amount > 0 && filter.IsInStock == true;
                }
                if (result && filterModel.MinPrice != null)
                {
                    result = x.Price > filter.MinPrice;
                }
                if (result && filterModel.MaxPrice != null)
                {
                    result = x.Price < filter.MaxPrice;
                }
                if (result && filterModel.Types != null)
                {
                    result = filter.Types.Contains(x.Type);
                }
                if (result && filterModel.Subjects != null)
                {
                    result = filter.Subjects.Contains(x.Subject);
                }
                if (!string.IsNullOrWhiteSpace(search))
                {
                    result = x.Id.ToString().Contains(search, StringComparison.OrdinalIgnoreCase)
                                                        || x.Name.Contains(search, StringComparison.OrdinalIgnoreCase)
                                                        || x.Description.Contains(search, StringComparison.OrdinalIgnoreCase);
                }
                return result;
            });
            // Sorting
            if (filter.SortingType != null)
            {
                if(filter.SortingType == SortingType.PriceASC)
                {
                    items = items.OrderBy(x => x.Price);
                }
                else if(filter.SortingType == SortingType.PriceDESC)
                {
                    items = items.OrderByDescending(x => x.Price);
                }
            }

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
