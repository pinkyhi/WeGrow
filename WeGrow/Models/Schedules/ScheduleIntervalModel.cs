namespace WeGrow.Models.Schedules
{
    public class ScheduleIntervalModel
    {
        public int From { get; set; }
        public int To { get; set; }
        public Dictionary<int, double> DayValues { get; set; }
    }
}
