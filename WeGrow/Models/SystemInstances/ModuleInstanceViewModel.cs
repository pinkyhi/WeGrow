namespace WeGrow.Models.SystemInstances
{
    public class ModuleInstanceViewModel
    {
        public string Id { get; set; }
        public string System_Id { get; set; }
        public int Module_Id { get; set; }
        public string ModuleName { get; set; }
        public DateTime LastResponse { get; set; }
    }
}
