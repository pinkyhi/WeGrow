using WeGrow.Models.Schedules;

namespace WeGrow.Models.SystemInstances
{
    public class CreateSystemRequest
    {
        public string Name { get; set; }
        public List<ModuleScheduleModel> ModuleSchedules { get; set; } = new();
        public List<ModuleInstanceViewModel> ModulesList { get; set; } = new();

    }
}
