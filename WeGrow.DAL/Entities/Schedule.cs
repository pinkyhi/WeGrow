namespace WeGrow.DAL.Entities
{
    public class Schedule
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string BlobLink { get; set; }
        public string BlobName { get; set; }
        public int TotalDays { get; set; }
        public bool Is_Public { get; set; }
        public string User_Id { get; set; }


        public List<SystemInstance> Systems { get; set; }
        public IEnumerable<Grow> Grows { get; set; }
    }
}
