﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Reflection;
using WeGrow.Core.Enums;
using WeGrow.Core.Helpers;
using WeGrow.Core.Resources;
using WeGrow.DAL.Entities;
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
        private readonly int pageSize = 6;
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
                    var ukName = ModulesResource.ResourceManager.GetString(x.Name);
                    var ukDesc = ModulesResource.ResourceManager.GetString(x.Description);

                    result = x.Id.ToString().Contains(search, StringComparison.OrdinalIgnoreCase)
                                                        || x.Name.Contains(search, StringComparison.OrdinalIgnoreCase) || ukName.Contains(search, StringComparison.OrdinalIgnoreCase)
                                                        || x.Description.Contains(search, StringComparison.OrdinalIgnoreCase) || ukDesc.Contains(search, StringComparison.OrdinalIgnoreCase);
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

        [Authorize]
        [Route("modules")]
        [HttpPost]
        public async Task<IActionResult> Order([FromBody] Dictionary<int, int> idCount)
        {
            Order newOrder = new Order()
            {
                Date = DateTime.Now,
                User_Id = HttpContext.Request.Headers.First(x => x.Key == ConstNames.Uid).Value
            };
            Dictionary<DAL.Entities.Module, int> order = new();
            foreach(var item in idCount)
            {
                var module = await repository.GetAsync<DAL.Entities.Module>(false, x => x.Id == item.Key);
                module.Amount -= item.Value;
                if(module.Amount < 0)
                {
                    throw new Exception($"Not enough amount of module #{item.Key}");
                }
                order.Add(module, item.Value);
            }
            List<Receipt> receipts = order.Select(x =>
            {
                var receipt = new Receipt()
                {
                    Amount = x.Value,
                    Module_Id = x.Key.Id,
                    Cache_Price = x.Key.Price,
                    Order = newOrder,
                };
                return receipt;
            }).ToList();
            newOrder.Receipts = receipts;
            newOrder = await repository.AddAsync(newOrder);
            await repository.UpdateRangeAsync(order.Keys);
            return Ok(newOrder.Id);
        }
    }
}
