using WeGrow.Models.Schedules;

namespace WeGrow.Models.SystemInstances
{
    public class SystemInstanceViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string User_Id { get; set; }
        public bool Is_Active { get; set; }

        public List<ModuleInstanceViewModel> ModuleInstances { get; set; } = new();
        public List<ModuleScheduleModel> ModuleSchedules { get; set; } = new();
        public SystemGrowModel LastGrow { get; set; }
    }
}
