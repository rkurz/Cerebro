using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dotnetCHARTING;
using Cerebro.Models.TargetProcess;
using Cerebro.ViewModels;

namespace Cerebro.Services
{
    public class ChartService
    {
        public static ChartViewModel BuildBurndownChart(Iteration iteration, List<Assignable> tasks)
        {
            SeriesCollection sc;
            Series s;

            sc = new SeriesCollection();

            s = new Series();
            s.Name = "Effort";
            s.Type = SeriesType.Bar;
            //s.DefaultElement.ShowValue = true;

            for (var day = iteration.StartDate.Date; day.Date <= iteration.EndDate.AddDays(1).Date; day = day.AddDays(1))
            {
                var remainingTime = 0.0;
                //Do not show values for future dates.
                if (day <= DateTime.Today.AddDays(1))
                {
                    foreach (var task in tasks)
                    {
                        if (task.Times.Items.Count == 0)
                        {
                            remainingTime += task.Effort;
                        }
                        else
                        {
                            //NOTE: The Remain property on a Time object represents only the remaining time for that role (ie/ developer/qa) so we can't use it here.
                            var timeSpent = task.Times.Items.Where(t => t.Date.Date < day).Sum(t => t.Spent);
                            remainingTime += task.Effort - timeSpent;
                        }
                    }
                }
                s.AddElements(new Element(day.ToShortDateString(), remainingTime));
            }

            sc.Add(s);

            var chart = InitializeBarGraph(sc, "Time Remaining");
            return new ChartViewModel
                        {
                            ImageFileName = string.Format("{0:s}/{1:s}.png", chart.TempDirectory, chart.FileName)
                        };
        }

        private static Chart InitializeBarGraph(SeriesCollection seriesCollection, string yAxisTitle)
        {
            var chart = new Chart();
            //chart.Title = "Burndown";
            chart.Type = ChartType.Combo;
            chart.TempDirectory = VirtualPathUtility.ToAbsolute("~/file/chart");
            chart.SeriesCollection.Add(seriesCollection);
            chart.YAxis.Label.Text = yAxisTitle;
            chart.YAxis.Label.Color = System.Drawing.ColorTranslator.FromHtml("#CCCCCC");
            chart.YAxis.DefaultTick.Label.Color = System.Drawing.ColorTranslator.FromHtml("#CCCCCC");
            chart.XAxis.DefaultTick.Label.Color = System.Drawing.ColorTranslator.FromHtml("#CCCCCC");
            chart.LegendBox.Visible = false;
            chart.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
            chart.TitleBox.Visible = false;
            chart.Background.Color = System.Drawing.ColorTranslator.FromHtml("#333333");
            chart.DefaultSeries.Element.Color = System.Drawing.ColorTranslator.FromHtml("#1B12A6");
            chart.DefaultElement.Color = System.Drawing.ColorTranslator.FromHtml("#1B12A6");
            chart.Width = new System.Web.UI.WebControls.Unit(600, System.Web.UI.WebControls.UnitType.Pixel);
            chart.Height = new System.Web.UI.WebControls.Unit(400, System.Web.UI.WebControls.UnitType.Pixel);
            chart.Font.Name = "Helvetica";
            chart.Font.Size = new System.Web.UI.WebControls.FontUnit(24, System.Web.UI.WebControls.UnitType.Pixel);
            chart.YAxis.Label.Font = new System.Drawing.Font("Helvetica", 8);
            chart.YAxis.DefaultTick.Label.Font = new System.Drawing.Font("Helvetica", 8);
            chart.XAxis.DefaultTick.Label.Font = new System.Drawing.Font("Helvetica", 8);

            //NOTE: needed to do this for the old version of .net charting (3.4).
            chart.FileManager.TempDirectory = VirtualPathUtility.ToAbsolute("~/file/chart");
            chart.FileManager.SaveImage(chart.GetChartBitmap());

            //chart.FileManager.FileName = chart.FileManager.TempDirectory + "/" + chart.FileManager.FileName + ".png";
            return chart;
        }
    }
}