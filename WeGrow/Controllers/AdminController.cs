using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeGrow.DAL.Entities;
using WeGrow.DAL.Interfaces;
using WeGrow.Models.Entities;

namespace WeGrow.Controllers
{
    [Route("admin")]
    [Authorize(Policy = "AdminPolicy")]
    public class AdminController : ControllerBase
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public AdminController(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [Route("modules")]
        [HttpGet]
        public async Task<IActionResult> Modules()
        {
            var modules = await repository.GetRangeAsync<Module>(false, x => true);
            var modulesEntities = modules.Select(x => mapper.Map<ModuleEntity>(x));
            return Ok(modulesEntities);
        }
    }
}
