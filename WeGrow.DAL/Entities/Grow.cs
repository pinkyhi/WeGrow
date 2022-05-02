using WeGrow.Core.Enums;

namespace WeGrow.DAL.Entities
{
    public class Grow
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public string Timelaps { get; set; }
        public int Schedule_Id { get; set; }
        public string System_Id { get; set; }
        public GrowStatus Status { get; set; }
        public string HistoryFile { get; set; }
        public bool IsPublic { get; set; }
        public string BlobLink { get; set; }
        public string BlobName { get; set; }

        public Schedule Schedule { get; set; }
        public SystemInstance System { get; set; }
    }
}
