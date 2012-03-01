using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cerebro.Models;
using dotnetCHARTING;
using Cerebro.DataFactories;

namespace Cerebro.Services
{
    public class StatusBoardService
    {
        public static StatusBoardViewModel BuildStatusBoardViewModel()
        {
            var model = new StatusBoardViewModel();

            var currentIteration = TargetProcessFactory.GetCurrentIteration();
            if (currentIteration == null)
                return model;

            model.BurndownChart = GetBurndownChart(currentIteration);
            model.TaskList = GetTaskList(currentIteration);
            model.TestCaseSummary = GetTestCaseSummary(currentIteration);
            return model;
        }

        private static TestCaseSummary GetTestCaseSummary(Iteration iteration)
        {
            var testCases = TargetProcessFactory.GetTestCases(iteration);
            var passedCount = testCases.Count(tc => tc.LastStatus == "True");
            var failedCount = testCases.Count(tc => tc.LastStatus == "False");
            var notRunCount = testCases.Count(tc => tc.LastStatus == null);
            return new TestCaseSummary
                        {
                            PassedCount = passedCount,
                            FailedCount = failedCount,
                            NotRunCount = notRunCount
                        };
        }

        private static List<TaskListItem> GetTaskList(Iteration iteration)
        {
            var report = new List<TaskListItem>();
            var userStories = TargetProcessFactory.GetUserStoriesForCurrentIteration(iteration);

            foreach (var story in userStories)
            {
                report.Add(new TaskListItem
                                {
                                    TaskId = story.Id,
                                    Name = story.Name,
                                    Status = GetTaskStatus(story.EntityState.Name)
                                });
            }
            return report;
        }

        private static TaskStatus GetTaskStatus(string entityStateName)
        {
            switch (entityStateName)
            {
                case "Pending":
                    return TaskStatus.NotStarted;
                case "Complete":
                    return TaskStatus.Done;
                default:
                    return TaskStatus.InProgress;
            }
        }

        private static Chart GetBurndownChart(Iteration iteration)
        {
            var tasks = TargetProcessFactory.GetTasksForCurrentIteration();
            return BuildBurndownChart(iteration, tasks);
        }

        private static Chart BuildBurndownChart(Iteration iteration, List<Assignable> tasks)
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
                if (day <= DateTime.Today)
                {
                    foreach (var task in tasks)
                    {
                        if (task.Times.Items.Count == 0)
                        {
                            remainingTime += task.Effort;
                        }
                        else
                        {
                            //var timeEntry = task.Times.Items.Where(t => t.Date.Date == day).OrderByDescending(t => t.Date).FirstOrDefault();
                            //var timeEntry = task.Times.Items.Where(t => t.Date.Date <= day).OrderByDescending(t => t.Date).FirstOrDefault();
                            //NOTE: The Remain property on a Time object represents only the remaining time for that role (ie/ developer/qa) so we can't use it here.
                            var timeSpent = task.Times.Items.Where(t => t.Date.Date < day).Sum(t => t.Spent);
                            remainingTime += task.Effort - timeSpent;
                            //if (timeEntry == null)
                            //{
                            //    remainingTime += task.Effort;
                            //}
                            //else
                            //{
                            //    remainingTime += timeEntry.Remain;
                            //}

                        }
                    }
                }
                s.AddElements(new Element(day.ToShortDateString(), remainingTime));
            }

            sc.Add(s);

            //var path = HttpContext.Current.Server.MapPath("/file/chart");
            var chart = new Chart
            {
                Title = "Burndown",
                Type = ChartType.Combo,
                TempDirectory = VirtualPathUtility.ToAbsolute("~/file/chart")
            };
            chart.SeriesCollection.Add(sc);
            chart.YAxis.Label.Text = "Time Remaining";
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