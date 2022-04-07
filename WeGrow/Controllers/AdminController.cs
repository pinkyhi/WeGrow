using Microsoft.AspNetCore.Mvc;
using WeGrow.DAL.Entities;
using WeGrow.DAL.Interfaces;

namespace WeGrow.Controllers
{
    [Route("admin")]
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
            return Ok(modules);
        }
    }
}
