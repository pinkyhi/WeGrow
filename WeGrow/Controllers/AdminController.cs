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
        public async Task<IActionResult> GetModules()
        {
            var modules = await repository.GetRangeAsync<Module>(false, x => true);
            var modulesEntities = modules.Select(x => mapper.Map<ModuleEntity>(x));
            return Ok(modulesEntities);
        }
        [Route("modules")]
        [HttpDelete]
        public async Task<IActionResult> DeleteModule(ModuleEntity deleteItem)
        {
            var exemplar = await repository.GetAsync<Module>(true, x => x.Id == deleteItem.Id);
            await repository.DeleteAsync(exemplar);
            return Ok();
        }

        [Route("modules")]
        [HttpPost]
        public async Task<IActionResult> AddModule([FromBody] ModuleEntity addItem)
        {
            Module newModule = mapper.Map<Module>(addItem);
            var exemplar = await repository.AddAsync(newModule);
            return Ok(mapper.Map<ModuleEntity>(exemplar));
        }

        [Route("modules")]
        [HttpPatch]
        public async Task<IActionResult> EditModule([FromBody] ModuleEntity addItem)
        {
            var exemplar = await repository.GetAsync<Module>(true, x => x.Id == addItem.Id);
            mapper.Map(addItem, exemplar);
            await repository.UpdateAsync(exemplar);
            return Ok();
        }
    }
}
