using AutoMapper;
using System.ComponentModel.DataAnnotations;
using WeGrow.Core.Enums;
using WeGrow.DAL.Entities;

namespace WeGrow.Models.Entities
{
    [AutoMap(typeof(Module), ReverseMap = true)]
    public class ModuleEntity
    {
        [Key]
        public int Id { get; set; }
        public ModuleType Type { get; set; }
        public ModuleSubject Subject { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string BlobLink { get; set; }
        public int Amount { get; set; }
    }
}
