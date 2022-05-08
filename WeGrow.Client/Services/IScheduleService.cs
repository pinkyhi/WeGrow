using WeGrow.Models.Schedules;

namespace WeGrow.Client.Services
{
    public interface IScheduleService
    {
        public List<(int from, int to)> GetMissingIntervals(int daysCount, List<ScheduleIntervalModel> intervals);
    }
}
