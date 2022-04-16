using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeGrow.DAL.Entities
{
    public class Receipt
    {
        public int Order_Id { get; set; }
        public int Module_Id { get; set; }
        public string Cache_System_Id { get; set; }
        [Min(0)]
        public int Amount { get; set; }
        [Min(0)]
        public decimal Cache_Price { get; set; }

        public Order Order { get; set; }
        public Module Module { get; set; }
    }
}
