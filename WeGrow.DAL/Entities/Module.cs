using System.ComponentModel.DataAnnotations;
using WeGrow.Core.Enums;

namespace WeGrow.DAL.Entities
{
    public class Module
    {
        public int Id { get; set; }
        public ModuleType Type { get; set; }
        public ModuleSubject Subject { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0, Double.PositiveInfinity)]
        public decimal Price { get; set; }
        [Range(0, Double.PositiveInfinity)]
        public int Amount { get; set; }
        public string BlobLink { get; set; }
        public string BlobName { get; set; }

        public bool Is_Public { get; set; }

        public IEnumerable<ModuleInstance> ModuleInstances { get; set; }
        public IEnumerable<Receipt> Receipts { get; set; }
    }
}
