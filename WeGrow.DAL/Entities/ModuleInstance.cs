using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeGrow.DAL.Entities
{
    public class ModuleInstance
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string System_Id { get; set; }
        public int Module_Id { get; set; }
        public DateTime LastResponse { get; set; }

        public Module Module { get; set; }
        public SystemInstance System { get; set; }
    }
}
