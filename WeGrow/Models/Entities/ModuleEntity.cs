using AutoMapper;
using WeGrow.Core.Enums;
using WeGrow.DAL.Entities;

namespace WeGrow.Models.Entities
{
    [AutoMap(typeof(Module), ReverseMap = true)]
    public class ModuleEntity
    {
        public ModuleEntity()
        {

        }

        public ModuleEntity(Module module)
        {
            Id = module.Id;
            Type = module.Type;
            Subject = module.Subject;
            Title = module.Title;
            Description = module.Description;
            Price = module.Price;
            Amount = module.Amount;
        }
        public int Id { get; set; }
        public ModuleType Type { get; set; }
        public ModuleSubject Subject { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
    }
}
