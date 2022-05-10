using System.ComponentModel.DataAnnotations.Schema;

namespace WeGrow.DAL.Entities
{
    public class SystemInstance
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string User_Id { get; set; }
        public bool Is_Active { get; set; }
        public int? ScheduleId { get; set; }

        public IEnumerable<ModuleInstance> ModuleInstances { get; set; }
        public List<Grow> Grows { get; set; }
        public Schedule Schedule { get; set; }
    }
}
