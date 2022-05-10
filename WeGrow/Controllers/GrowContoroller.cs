using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeGrow.Core.Resources;
using WeGrow.DAL.Entities;
using WeGrow.DAL.Interfaces;
using WeGrow.Interfaces;

namespace WeGrow.Controllers
{
    [Authorize]
    [Route("/grows")]
    public class GrowContoroller : Controller
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;
        private readonly IBlobService blobService;

        public GrowContoroller(IRepository repository, IMapper mapper, IBlobService blobService)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.blobService = blobService;
        }

        // Example TODO: change
        [HttpPost]
        public async Task<IActionResult> StartGrowForSystem([FromBody] string systemId)
        {
            var userId = HttpContext.Request.Headers.First(x => x.Key == ConstNames.Uid).Value;
            var item = await repository.GetAsync<SystemInstance>(true, x => x.Id.Equals(systemId) && x.User_Id.Equals(userId));
            if (item == null)
            {
                return BadRequest();
            }
            else
            {
                item.Is_Active = true;
                await repository.UpdateAsync(item);
            }

            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> StopGrowForSystem([FromBody] string systemId)
        {
            var userId = HttpContext.Request.Headers.First(x => x.Key == ConstNames.Uid).Value;
            var item = await repository.GetAsync<SystemInstance>(true, x => x.Id.Equals(systemId) && x.User_Id.Equals(userId));
            if (item == null)
            {
                return BadRequest();
            }
            else
            {
                item.Is_Active = false;
                await repository.UpdateAsync(item);
            }

            return Ok();
        }
    }
}
