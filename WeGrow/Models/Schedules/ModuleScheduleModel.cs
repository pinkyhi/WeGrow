using WeGrow.Core.Enums;

namespace WeGrow.Models.Schedules
{
    public class ModuleScheduleModel
    {
        public string ModuleInstanceId { get; set; }

        public ModuleSubject ModuleSubject { get; set; }

        public string ModuleName { get; set; }

        public int DaysCount { get; set; }

        public List<ScheduleIntervalModel> Intervals { get; set; } = new();
    }
}
