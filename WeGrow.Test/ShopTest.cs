using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeGrow.Client.Services;
using Xunit;

namespace WeGrow.Test
{
    public class ShopTest
    {
        [Fact]
        public void CreateExtendedChartTest()
        {
            var chartService = new ChartService();

            var sourceChart = new Dictionary<int, double>();
            sourceChart.Add(0, 10);
            sourceChart.Add(24, 10);

            chartService.CreateExtendedChart(sourceChart, out Dictionary<int, double> destChart);

            Assert.Equal(25, destChart.Count);
        }

        [Fact]
        public void GetExactValue()
        {
            var chartService = new ChartService();

            var sourceChart = new Dictionary<int, double>();
            sourceChart.Add(0, 10);
            sourceChart.Add(24, 10);

            chartService.CreateExtendedChart(sourceChart, out Dictionary<int, double> destChart);

            Assert.Equal(10, chartService.GetExactValue(destChart, 12.5));
        }

        [Fact]
        public void GetDayChart()
        {
            var chartService = new ChartService();

            var sourceChart = new Dictionary<int, double>();
            sourceChart.Add(0, 10);
            sourceChart.Add(24, 10);
            chartService.CreateExtendedChart(sourceChart, out Dictionary<int, double> destChart);
            var dayChart = chartService.GetDayChart(destChart);

            Assert.Equal(25, dayChart.Count);
        }
    }
}
