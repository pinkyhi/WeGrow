using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        [Min(0)]
        public decimal Price { get; set; }
        [Min(0)]
        public int Amount { get; set; }
        public bool Is_Public { get; set; }

        public IEnumerable<ModuleInstance> ModuleInstances { get; set; }
        public IEnumerable<Receipt> Receipts { get; set; }
    }
}
