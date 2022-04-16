using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeGrow.DAL.Entities
{
    public class Schedule
    {
        public int Id { get; set; }
        public string System_Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string File { get; set; }
        public TimeSpan Duration { get; set; }
        public bool Is_Public { get; set; }

        public SystemInstance System { get; set; }
        public IEnumerable<Grow> Grows { get; set; }
    }
}
