using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using WeGrow.Core.Resources;
using WeGrow.DAL.Entities;
using WeGrow.DAL.Interfaces;
using WeGrow.Interfaces;
using WeGrow.LiqPay.Interfaces;
using WeGrow.Models.Schedules;
using WeGrow.Models.SystemInstances;

namespace WeGrow.Controllers
{
    [Authorize]
    [Route("/account")]
    public class AccountController : Controller
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;
        private readonly IBlobService blobService;


        public AccountController(IRepository repository, IMapper mapper, IBlobService blobService)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.blobService = blobService;
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
            var items = await repository.GetRangeAsync<SystemInstance>(false, x => x.User_Id.Equals(userId), y => y.Include(i => i.Schedule));
            var models = new List<SystemInstanceViewModel>();

            foreach (var item in items)
            {
                var scheduleBlob = blobService.GetBlobAsync(ConstNames.Blob.Schedules, item.Schedule.BlobName);
                var model = mapper.Map<SystemInstanceViewModel>(item);
                model.ModuleSchedules = JsonConvert.DeserializeObject<List<ModuleScheduleModel>>(Encoding.ASCII.GetString(scheduleBlob.Result.Content));
                models.Add(model);
            }
            models = models.OrderBy(x => x.Is_Active).ThenBy(x => x.Name).ToList();

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

        [Route("systems")]
        [HttpPatch]
        public async Task<IActionResult> ChangeActiveStatus([FromBody] string id)
        {
            var userId = HttpContext.Request.Headers.First(x => x.Key == ConstNames.Uid).Value;
            var item = await repository.GetAsync<SystemInstance>(true, x => x.Id.Equals(id) && x.User_Id.Equals(userId));
            if (item == null)
            {
                return BadRequest();
            }
            else
            {
                item.Is_Active = !item.Is_Active;
                await repository.UpdateAsync(item);
            }

            return Ok();
        }

        [Route("systems")]
        [HttpPost]
        public async Task<IActionResult> CreateSystemWithSchedule([FromBody] CreateSystemRequest creationModel)
        {
            var userId = HttpContext.Request.Headers.First(x => x.Key == ConstNames.Uid).Value;
            var moduleInstances = await repository.GetRangeAsync<ModuleInstance>(true, x => creationModel.ModuleSchedules.Any(y => y.ModuleInstanceId.Equals(x.Id)), z => z.Include(item => item.System).Include(item => item.Module));

            // Schedule uploading
            var newFileName = $"{Guid.NewGuid()}.json";

            Schedule newSchedule = new()
            {
                Name = creationModel.Name,
            };

            try
            {
                string jsonStr = JsonConvert.SerializeObject(creationModel.ModuleSchedules);
                var uploadResult = await blobService.UploadFileBlobAsync(ConstNames.Blob.Schedules, newFileName, Encoding.ASCII.GetBytes(jsonStr), "application/json");
                var newBlobLink = blobService.GetBlobLinkAsync(ConstNames.Blob.Schedules, newFileName);
                newSchedule.BlobName = newFileName;
                newSchedule.BlobLink = newBlobLink.ToString();
            }
            catch
            {
                BadRequest();
            }

            newSchedule = await repository.AddAsync(newSchedule);
            SystemInstance newSystem = new()
            {
                Name = creationModel.Name,
                User_Id = userId,
                Schedule = newSchedule,
                ModuleInstances = moduleInstances.ToList()
            };
            newSystem = await repository.AddAsync(newSystem);
            var systemModel = mapper.Map<SystemInstanceViewModel>(newSystem);
            systemModel.ModuleSchedules = creationModel.ModuleSchedules;
            return Ok(systemModel);
        }
    }
}
