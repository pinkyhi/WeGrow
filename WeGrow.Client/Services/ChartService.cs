namespace WeGrow.Client.Services
{
    public class ChartService : IChartService
    {
        public void CreateExtendedChart(Dictionary<int, double> sourceChart, out Dictionary<int, double> destChart)
        {
            destChart = new Dictionary<int, double>();
            if(sourceChart.Count >= 2)
            {
                sourceChart = sourceChart.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
                for(int i = sourceChart.First().Key; i <= sourceChart.Last().Key; i++)
                {
                    if(sourceChart.TryGetValue(i, out double currentValue)) // Hits on first and last in 100%
                    {
                        destChart.Add(i, currentValue);
                    }
                    else
                    {
                        var nextKV = sourceChart.First(x => x.Key > i);
                        var prevKV = destChart.Last();
                        destChart.Add(i, GetValueBetween(prevKV.Key, prevKV.Value, nextKV.Key, nextKV.Value, i));
                    }
                }
            }
            else
            {
                throw new ArgumentException("Chart has to have more than 1 point");
            }
        }

        public Dictionary<string, double> GetDayChart(Dictionary<int, double> sourceChart)
        {
            var result = new Dictionary<string, double>();
            foreach(var item in sourceChart)
            {
                var ts = new TimeSpan(item.Key, 0, 0);
                var str = ts.TotalHours == 24 ? "24:00" : ts.ToString(@"hh\:mm");
                result.Add(str, item.Value);
            }
            return result;
        }

        public double GetExactValue(Dictionary<int, double> sourceChart, double position)
        {
            double result = 0;
            if (sourceChart.Count >= 2)
            {
                sourceChart = sourceChart.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);

                try
                {
                    var prevItem = sourceChart.First(x => x.Key < position);
                    var nextItem = sourceChart.First(x => x.Key > position);
                    result = Math.Round(((nextItem.Value - prevItem.Value) * (((double)(position - prevItem.Key))/(nextItem.Key - prevItem.Key))) + prevItem.Value, 3);
                }
                catch
                {
                    throw new ArgumentOutOfRangeException("Position is out of chart");
                }
            }
            else
            {
                throw new ArgumentException("Chart has to have more than 1 point");
            }
            return result;
        }

        private double GetValueBetween(int x1, double y1, int x2, double y2, int currentX)
        {
            return Math.Round(((y2 - y1) * (((double)(currentX - x1)) / (x2 - x1))) + y1, 3);

        }
    }
}
