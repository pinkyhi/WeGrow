namespace WeGrow.Models.SystemInstances
{
    public class SystemInstanceViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string User_Id { get; set; }
        public bool Is_Active { get; set; }

        public List<ModuleInstanceViewModel> ModuleInstances { get; set; } = new();
    }
}
