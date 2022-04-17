using AutoMapper;
using System.ComponentModel.DataAnnotations;
using WeGrow.DAL.Entities;

namespace WeGrow.Models.Entities
{
    [AutoMap(typeof(Schedule), ReverseMap = true)]
    public class ScheduleEntity
    {
        [Key]
        public int Id { get; set; }
        public string System_Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string File { get; set; }
        public TimeSpan Duration { get; set; }
        public bool Is_Public { get; set; }
    }
}
