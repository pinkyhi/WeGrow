using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeGrow.Core.Resources;
using WeGrow.DAL.Entities;
using WeGrow.DAL.Interfaces;
using WeGrow.Interfaces;
using WeGrow.Models.SystemInstances;

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
                var maxStart = item.Grows.Max(i => i.StartDate);
                var lastGrow = item.Grows.FirstOrDefault(x => x.StartDate.Equals(maxStart));
                if(lastGrow?.Status != Core.Enums.GrowStatus.Succeded)
                {
                    if (!lastGrow.TimelapsBlobName.Equals(ConstNames.Blob.DefaultTimelapsName))
                    {
                        await blobService.DeleteBlobAsync(ConstNames.Blob.Timelaps, lastGrow.TimelapsBlobName);
                    }
                    // await blobService.DeleteBlobAsync(ConstNames.Blob.Grows, lastGrow.GrowBlobName);

                    item.Grows.Remove(lastGrow);
                    item.Is_Active = false;
                    await repository.UpdateAsync(item);

                    await repository.DeleteAsync(lastGrow);
                }
                else
                {
                    item.Is_Active = false;
                    await repository.UpdateAsync(item);
                }
            }

            return Ok();
        }
    }
}
