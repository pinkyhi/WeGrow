using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeGrow.Core.Resources;
using WeGrow.DAL.Entities;
using WeGrow.DAL.Interfaces;
using WeGrow.LiqPay.Interfaces;
using WeGrow.Models.SystemInstances;

namespace WeGrow.Controllers
{
    [Authorize]
    [Route("/account")]
    public class AccountController : Controller
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;
        private readonly ILiqPayService liqPayService;

        public AccountController(IRepository repository, IMapper mapper, ILiqPayService liqPayService)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.liqPayService = liqPayService;
        }

        [Route("modules")]
        [HttpGet]
        public async Task<IActionResult> GetModules()
        {
            var userId = HttpContext.Request.Headers.First(x => x.Key == ConstNames.Uid).Value;
            var orders = await repository.GetRangeAsync<ModuleInstance>(false, x => x.User_Id.Equals(userId), y => y.Include(i => i.Module));
            var models = orders.Select(x => mapper.Map<ModuleInstanceViewModel>(x)).OrderBy(x => x.System_Id);

            return Ok(models);
        }
        [Route("systems")]
        [HttpGet]
        public async Task<IActionResult> GetSystems()
        {
            var userId = HttpContext.Request.Headers.First(x => x.Key == ConstNames.Uid).Value;
            var items = await repository.GetRangeAsync<SystemInstance>(false, x => x.User_Id.Equals(userId), y => y.Include(i => i.Schedules));
            var models = items.Select(x => mapper.Map<SystemInstanceViewModel>(x)).OrderBy(x => x.Is_Active).ThenBy(x => x.Name);

            return Ok(models);
        }

        [Route("systems")]
        [HttpDelete]
        public async Task<IActionResult> DeleteSystem(string id)
        {
            var userId = HttpContext.Request.Headers.First(x => x.Key == ConstNames.Uid).Value;
            var item = await repository.GetAsync<SystemInstance>(true, x => x.Id.Equals(id) && x.User_Id.Equals(userId), y => y.Include(i => i.ModuleInstances));
            if(item == null)
            {
                return BadRequest();
            }
            if(item.ModuleInstances.Count() > 0)
            {
                await repository.DeleteAsync(item);
            }

            return Ok();
        }
    }
}
