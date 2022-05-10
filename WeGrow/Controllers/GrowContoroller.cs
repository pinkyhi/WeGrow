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
using WeGrow.Models.Grow;
using WeGrow.Models.Schedules;
using WeGrow.Models.SystemInstances;
using WeGrow.Temp;

namespace WeGrow.Controllers
{
    [Authorize]
    [Route("/grows")]
    public class GrowContoroller : Controller
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;
        private readonly IBlobService blobService;
        private readonly IChartService chartService;

        public GrowContoroller(IRepository repository, IMapper mapper, IBlobService blobService, IChartService chartService)
        {
            this.chartService = chartService;
            this.repository = repository;
            this.mapper = mapper;
            this.blobService = blobService;
        }

        // Example TODO: change
        [HttpPost]
        public async Task<IActionResult> StartGrowForSystem([FromBody] string systemId)
        {
            var userId = HttpContext.Request.Headers.First(x => x.Key == ConstNames.Uid).Value;
            var item = await repository.GetAsync<SystemInstance>(true, x => x.Id.Equals(systemId) && x.User_Id.Equals(userId), y => y.Include(i => i.Schedule));
            Grow newGrow = null;
            if (item == null)
            {
                return BadRequest();
            }
            else
            {
                newGrow = new Grow() {
                    Name = item.Name + DateTime.Now.ToShortTimeString(),
                    Schedule = item.Schedule,
                    System = item,
                    ScheduleBlobLink = item.Schedule.BlobLink,
                    ScheduleBlobName = item.Schedule.BlobName,
                    StartDate = DateTime.Now,
                    Status = Core.Enums.GrowStatus.Processing,
                    TimelapsBlobLink = "https://wegrowblob.blob.core.windows.net/timelaps/defaultGrowTimelaps.gif",
                    TimelapsBlobName = ConstNames.Blob.DefaultTimelapsName,
                    TotalDays = item.Schedule.TotalDays
                };
                newGrow = await repository.AddAsync(newGrow);
                item.Is_Active = true;
                await repository.UpdateAsync(item);
            }

            return Ok(mapper.Map<SystemGrowModel>(newGrow));
        }

        [HttpPatch]
        public async Task<IActionResult> StopGrowForSystem([FromBody] string systemId)
        {
            var userId = HttpContext.Request.Headers.First(x => x.Key == ConstNames.Uid).Value;
            var item = await repository.GetAsync<SystemInstance>(true, x => x.Id.Equals(systemId) && x.User_Id.Equals(userId), y => y.Include(i => i.Grows));
            if (item == null)
            {
                return BadRequest();
            }
            else
            {
                if(item.Grows.Count > 0)
                {
                    var maxStart = item.Grows.Max(i => i.StartDate);
                    var lastGrow = item.Grows.FirstOrDefault(x => x.StartDate.Equals(maxStart));
                    if (lastGrow?.Status != Core.Enums.GrowStatus.Succeded)
                    {
                        if (!lastGrow.TimelapsBlobName.Equals(ConstNames.Blob.DefaultTimelapsName))
                        {
                            await blobService.DeleteBlobAsync(ConstNames.Blob.Timelaps, lastGrow.TimelapsBlobName);
                        }
                        // await blobService.DeleteBlobAsync(ConstNames.Blob.Grows, lastGrow.GrowBlobName);
                        await repository.DeleteAsync(lastGrow);

                        item.Grows.Remove(lastGrow);
                        item.Is_Active = false;
                        await repository.UpdateAsync(item);

                    }
                }
                else
                {
                    item.Is_Active = false;
                    await repository.UpdateAsync(item);
                }
            }

            return Ok();
        }

        // Temporary realisation
        [Route("module")]
        [HttpGet]
        public async Task<IActionResult> GetModuleUpdate(string id)
        {
            var response = new ModuleUpdateModel();
            var userId = HttpContext.Request.Headers.First(x => x.Key == ConstNames.Uid).Value;
            var item = await repository.GetAsync<ModuleInstance>(false, x => x.Id.Equals(id) && x.User_Id.Equals(userId), y => y.Include(i => i.System).ThenInclude(i => i.Schedule).Include(i => i.System).ThenInclude(i => i.Grows));
            if (item == null || item.System.Grows.Count == 0)
            {
                return BadRequest();
            }
            else
            {
                var scheduleBlob = blobService.GetBlobAsync(ConstNames.Blob.Schedules, item.System.Schedule.BlobName);
                var moduleSchedules = JsonConvert.DeserializeObject<List<ModuleScheduleModel>>(Encoding.ASCII.GetString(scheduleBlob.Result.Content));
                var moduleSchedule = moduleSchedules?.FirstOrDefault(x => x.ModuleInstanceId.Equals(id));
                if(moduleSchedule == null)
                {
                    return BadRequest();
                }

                var maxStart = item.System.Grows.Max(i => i.StartDate);
                var lastGrow = item.System.Grows.First(x => x.StartDate.Equals(maxStart));
                if (lastGrow.Status != Core.Enums.GrowStatus.Processing)
                {
                    return BadRequest();
                }

                var daysGone = (DateTime.Now - lastGrow.StartDate).TotalDays;

                var currentInterval = moduleSchedule.Intervals.FirstOrDefault(x => x.From <= daysGone && x.To >= daysGone);
                if(currentInterval == null)
                {
                    return BadRequest();
                }
                var now = DateTime.Now;
                var currentPosition = now - new DateTime(now.Year, now.Month, now.Day);
                var currenValue = chartService.GetExactValue(currentInterval.DayPatternValues, currentPosition.TotalHours);

                item.LastResponse = DateTime.Now;
                item.LastResponseItem = Math.Round(currenValue, 3).ToString();
                response.LastResponse = item.LastResponse;
                response.LastResponseItem = item.LastResponseItem;
                await repository.UpdateAsync(item);
            }

            return Ok(response);
        }
    }
}
