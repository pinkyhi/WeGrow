using System.ComponentModel.DataAnnotations.Schema;

namespace WeGrow.DAL.Entities
{
    public class ModuleInstance
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string System_Id { get; set; }
        public int Module_Id { get; set; }
        public DateTime LastResponse { get; set; }
        public string LastResponseItem { get; set; }
        public string User_Id { get; set; }

        public Module Module { get; set; }
        public SystemInstance System { get; set; }
    }
}
