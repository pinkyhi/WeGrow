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
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
    }
}
