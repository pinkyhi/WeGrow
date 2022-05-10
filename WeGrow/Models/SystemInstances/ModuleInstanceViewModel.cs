using WeGrow.Core.Enums;

namespace WeGrow.Models.SystemInstances
{
    public class ModuleInstanceViewModel
    {
        public string Id { get; set; }
        public string System_Id { get; set; }
        public int Module_Id { get; set; }
        public string ModuleName { get; set; }
        public ModuleSubject Subject { get; set; }
        public string LastResponseItem { get; set; }
        public DateTime LastResponse { get; set; }
    }
}
