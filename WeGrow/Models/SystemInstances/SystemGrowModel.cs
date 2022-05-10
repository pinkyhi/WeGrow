using AutoMapper;
using WeGrow.Core.Enums;
using WeGrow.DAL.Entities;

namespace WeGrow.Models.SystemInstances
{
    [AutoMap(typeof(DAL.Entities.Grow), ReverseMap = true)]
    public class SystemGrowModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }

        public int Schedule_Id { get; set; }
        public string System_Id { get; set; }
        public GrowStatus Status { get; set; }
        public bool IsPublic { get; set; }
        public string ScheduleBlobLink { get; set; }
        public string ScheduleBlobName { get; set; }
        public string GrowBlobLink { get; set; }
        public string GrowBlobName { get; set; }
        public string TimelapsBlobLink{ get; set; }
        public string TimelapsBlobName { get; set; }
    }
}
