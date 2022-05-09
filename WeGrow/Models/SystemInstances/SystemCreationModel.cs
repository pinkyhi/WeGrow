using WeGrow.Models.Schedules;

namespace WeGrow.Models.SystemInstances
{
    public class SystemCreationModel
    {
        public CreationStep Step { get; set; }
        public string Name { get; set; }
        public HashSet<ModuleInstanceViewModel> Modules { get; set; } = new();
        public List<ModuleScheduleModel> ModuleSchedules { get; set; } = new();
        public enum CreationStep
        {
            None, SystemInfo, ModulesSelection, ScheduleInitialization, ScheduleCreation
        }
    }
}
