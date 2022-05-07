namespace WeGrow.Models.SystemInstances
{
    public class SystemCreationModel
    {
        public CreationStep Step { get; set; }
        public string Name { get; set; }
        public HashSet<ModuleInstanceViewModel> Modules { get; set; } = new();
        public enum CreationStep
        {
            None, SystemInfo, ModulesSelection, ScheduleCreation
        }
    }
}
