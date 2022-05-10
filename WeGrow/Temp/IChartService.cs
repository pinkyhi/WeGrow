namespace WeGrow.Temp
{
    public interface IChartService
    {
        public void CreateExtendedChart(Dictionary<int, double> sourceChart, out Dictionary<int, double> destChart);

        public double GetExactValue(Dictionary<int, double> sourceChart, double position);

        public Dictionary<string, double> GetDayChart(Dictionary<int, double> sourceChart);
    }
}
