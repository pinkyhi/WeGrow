using WeGrow.Models.Schedules;

namespace WeGrow.Client.Services
{
    public class ScheduleService : IScheduleService
    {
        public List<(int from, int to)> GetMissingIntervals(int daysCount, List<ScheduleIntervalModel> intervals)
        {
            List<(int from, int to)> result = new List<(int from, int to)> ();
            intervals = intervals.OrderBy(x => x.From).ToList();
            int expectedFrom = 0;
            if(intervals.Count > 0)
            {
                for (int i = 0; i < intervals.Count; i++)
                {
                    if (intervals[i].From != expectedFrom)
                    {
                        result.Add((expectedFrom, intervals[i].From - 1));
                    }
                    expectedFrom = intervals[i].To + 1;
                    if (i == intervals.Count - 1)
                    {
                        if (expectedFrom != daysCount)
                        {
                            result.Add((expectedFrom, daysCount - 1));
                        }
                    }
                }
            }
            else
            {
                result.Add((expectedFrom, daysCount - 1));
            }
            return result;
        }
    }
}
