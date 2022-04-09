using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeGrow.DAL.Entities;
using WeGrow.DAL.Interfaces;
using WeGrow.Models.Entities;

namespace WeGrow.Controllers
{
    [Route("admin")]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly IRepository repository;

        public AdminController(IRepository repository)
        {
            this.repository = repository;
        }

        [Route("modules")]
        [HttpGet]
        public async Task<IActionResult> Modules()
        {
            var modules = await repository.GetRangeAsync<Module>(false, x => true);
            var modulesEntities = modules.Select(x => new ModuleEntity(x));
            return Ok(modulesEntities);
        }
    }
}
